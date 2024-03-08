using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using log4net;
using Newtonsoft.Json;

namespace ActiveTimeTracker
{
    public static class Storage
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Storage));

        public static string DataDirectory = GetDataDirectory().FullName;
        private static string prevFileNameExtension = ".prev";

        private readonly static JsonSerializerSettings JsonSerializerSettings;

        static Storage()
        {
            JsonSerializerSettings = new JsonSerializerSettings();
            JsonSerializerSettings.DateFormatString = "yyyy'-'MM'-'dd'T'HH':'mm':'ssK";
            JsonSerializerSettings.Converters.Add(new TimespanConverter());
        }

        public static void SaveEntry(Entry entry)
        {
            AddDayEntry(entry);

            AddOrUpdateMonthEntry(entry);
        }

        private static void AddOrUpdateMonthEntry(Entry entry)
        {
            string path = GetFilePath(entry.Date.Year, entry.Date.Month);
            List<Entry> existingEntries = LoadEntries(path);
            SortedDictionary<DateTime, TimeSpan> sortedEntries = ToSortedDictionary(existingEntries);

            sortedEntries[entry.Date.Date] = entry.LoggedTime;
            var entries = sortedEntries.Select(kv => new Entry { Date = kv.Key, LoggedTime = kv.Value });

            SaveToJson(entries, path);
        }

        private static void AddDayEntry(Entry entry)
        {
            string path = GetFilePath(entry.Date.Year, entry.Date.Month, entry.Date.Day);
            List<Entry> entries = LoadEntries(path);

            entries.Add(entry);

            SaveToJson(entries, path);
        }

        private static void SaveToJson(IEnumerable<Entry> entries, string path)
        {
            string json = JsonConvert.SerializeObject(entries, Formatting.Indented, JsonSerializerSettings);
            string newFileName = path + ".new";
            string prevFileName = path + prevFileNameExtension;
            File.WriteAllText(newFileName, json, Encoding.UTF8);

            if(File.Exists(path))
            {
                File.Move(path, prevFileName);
            }
            File.Move(newFileName, path);
            File.Delete(prevFileName);
        }

        public static Entry GetEntry(DateTime date)
        {
            return GetEntries(date.Date, date.Date).SingleOrDefault();
        }

        public static List<Entry> GetEntries(DateTime start, DateTime end)
        {
            List<Entry> entries = new List<Entry>();
            int year = start.Year, month = start.Month;
            while (year < end.Year || (year <= end.Year && month <= end.Month))
            {
                List<Entry> monthEntries = LoadEntries(GetFilePath(year, month));
                entries.AddRange(monthEntries.Where(e => start <= e.Date && e.Date <= end));

                month++;
                if (12 < month)
                {
                    month = 1;
                    year++;
                }
            }

            return entries;
        }

        private static SortedDictionary<DateTime, TimeSpan> ToSortedDictionary(List<Entry> entries)
        {
            Dictionary<DateTime, TimeSpan> dict = entries.ToDictionary(e => e.Date, e => e.LoggedTime);
            return new SortedDictionary<DateTime, TimeSpan>(dict);
        }

        private static string BackupFileName(string file)
        {
            return file + "." + Guid.NewGuid().ToString().Substring(0, 8) + ".backup.json";
        }

        private static List<Entry> LoadEntries(string file, bool isRecovery = false)
        {
            if (File.Exists(file))
            {
                string json = File.ReadAllText(file, Encoding.UTF8);
                List<Entry> entries = JsonConvert.DeserializeObject<List<Entry>>(json);

                if (entries == null)
                {
                    log.Error($"File unexpectedly deserialized to empty result: {file}");
                    File.Move(file, BackupFileName(file));

                    if (!isRecovery)
                    {
                        string prevFileName = file + prevFileNameExtension;
                        log.Warn($"Attempting recovery from: {prevFileName}");

                        if(File.Exists(prevFileName))
                        {
                            File.Move(prevFileName, file);
                            return LoadEntries(file, true);
                        } else
                        {
                            log.Error("Recovery file not found");
                        }
                    }

                    log.Error($"Resetting file, you may want to check {file}.*.backup.json");
                    return new List<Entry>(0);
                }

                return entries;
            }

            return new List<Entry>(0);
        }

        private static string GetFilePath(string fileName)
        {
            return Path.Combine(DataDirectory, $"{fileName}.json");
        }

        private static string GetFilePath(int year, int month)
        {
            return GetFilePath($"{year}_{month:D2}");
        }

        private static string GetFilePath(int year, int month, int day)
        {
            return GetFilePath($"{year}_{month:D2}_{day:D2}");
        }

        private static DirectoryInfo GetDataDirectory()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string folder = Path.Combine(appData, "ActiveTimeTracker");
            return Directory.CreateDirectory(folder);
        }

        private class TimespanConverter : JsonConverter<TimeSpan>
        {
            public const string TimeSpanFormatString = @"hh\:mm\:ss";

            public override void WriteJson(JsonWriter writer, TimeSpan value, JsonSerializer serializer)
            {
                var timespanFormatted = $"{value.ToString(TimeSpanFormatString)}";
                writer.WriteValue(timespanFormatted);
            }

            public override TimeSpan ReadJson(JsonReader reader, Type objectType, TimeSpan existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                TimeSpan parsedTimeSpan;
                TimeSpan.TryParseExact((string)reader.Value, TimeSpanFormatString, null, out parsedTimeSpan);
                return parsedTimeSpan;
            }
        }
    }
}
