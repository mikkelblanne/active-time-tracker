using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ActiveTimeTracker
{
    public static class Storage
    {
        public static string DataDirectory = GetDataDirectory().FullName;

        public static void SaveEntry(Entry entry)
        {
            string path = GetFilePath(entry.Date.Year, entry.Date.Month);
            List<Entry> existingEntries = LoadEntries(path);
            SortedDictionary<DateTime, TimeSpan> sortedEntries = ToSortedDictionary(existingEntries);

            sortedEntries[entry.Date.Date] = entry.LoggedTime;
            var entries = sortedEntries.Select(kv => new Entry { Date = kv.Key, LoggedTime = kv.Value });

            string json = JsonConvert.SerializeObject(entries, Formatting.Indented);
            File.WriteAllText(path, json, Encoding.UTF8);
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

        private static List<Entry> LoadEntries(string file)
        {
            if (File.Exists(file))
            {
                string json = File.ReadAllText(file, Encoding.UTF8);
                List<Entry> entries = JsonConvert.DeserializeObject<List<Entry>>(json);
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

        private static DirectoryInfo GetDataDirectory()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string folder = Path.Combine(appData, "ActiveTimeTracker");
            return Directory.CreateDirectory(folder);
        }
    }
}
