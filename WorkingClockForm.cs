using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;

namespace WorkingClock
{
    public partial class WorkingClockForm : Form
    {
        public WorkingClockForm()
        {
            InitializeComponent();

            TryLoadingToday();
            StartDateRolloverTimer();
            StartAutoCounter();
            StartAutoSave();
        }

        private TimeSpan _unlockedTimeElapsed = TimeSpan.Zero;
        private DateTime _lastUnlock;
        private Timer _alwaysTimer;
        private Timer _saveTimer;
        private AbsoluteTimer.AbsoluteTimer _dateRolloverTimer;

        private TimeSpan GetCurrentElapsed()
        {
            TimeSpan currentElapsed = DateTime.Now - _lastUnlock;
            return _unlockedTimeElapsed + currentElapsed;
        }

        private void TryLoadingToday()
        {
            Entry today = Storage.GetEntry(DateTime.Now);
            if (today != null)
            {
                _unlockedTimeElapsed = today.LoggedTime;
            }
        }

        private void StartAutoCounter()
        {
            SystemEvents.SessionSwitch += SystemEventsOnSessionSwitch;

            _lastUnlock = DateTime.Now;
            _alwaysTimer = new Timer();
            _alwaysTimer.Interval = 1000;
            _alwaysTimer.Tick += _autoTimer_Tick;
            _alwaysTimer.Start();
        }

        private void _autoTimer_Tick(object sender, EventArgs e)
        {
            DisplayElapsedTime();
        }

        private void DisplayElapsedTime()
        {
            TimeSpan elapsed = _unlockedTimeElapsed;
            elapsed += DateTime.Now - _lastUnlock;
            label_unlockedTime.Text = FormatTimeSpan(elapsed);
        }

        private void StartAutoSave()
        {
            _saveTimer = new Timer();
            _saveTimer.Interval = 30000;
            _saveTimer.Tick += _saveTimer_Tick; ;
            _saveTimer.Start();
        }

        private void _saveTimer_Tick(object sender, EventArgs e)
        {
            Save();
        }

        private void StartDateRolloverTimer()
        {
            DateTime rolloverTime = DateTime.Now.AddDays(1).Date;
            Debug.WriteLine($"StartDateRolloverTimer scheduling reset at {rolloverTime}");
            _dateRolloverTimer = new AbsoluteTimer.AbsoluteTimer(rolloverTime, DateRollover, null);
        }

        private void DateRollover(object state)
        {
            Debug.WriteLine($"{DateTime.Now} DateRollover");
            // Leave whatever was saved last - it's already past midnight, so the wrong day would be saved now
            // Reset counters
            _unlockedTimeElapsed = TimeSpan.Zero;
            _lastUnlock = DateTime.Now;
            _dateRolloverTimer.Dispose();
            StartDateRolloverTimer();
        }

        private static string FormatTimeSpan(TimeSpan elapsed)
        {
            return elapsed.ToString("hh\\:mm\\:ss");
        }

        private void SystemEventsOnSessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            switch (e.Reason)
            {
                case SessionSwitchReason.SessionLock:
                    TimeSpan currentElapsed = DateTime.Now - _lastUnlock;
                    Debug.WriteLine($"SessionSwitch: Locked after {currentElapsed}");
                    _unlockedTimeElapsed += currentElapsed;
                    break;
                case SessionSwitchReason.SessionUnlock:
                    _lastUnlock = DateTime.Now;
                    Debug.WriteLine($"SessionSwitch: Unlocked at {_lastUnlock}");
                    break;
                default:
                    Debug.WriteLine($"SessionSwitch: {e.Reason}");
                    break;
            }
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            var entries = Storage.GetEntries(e.Start, e.End);
            TimeSpan total = entries.Aggregate(TimeSpan.Zero, (acc, entry) => acc + entry.LoggedTime);
            label_timeLogged.Text = FormatTimeSpan(total);
        }

        private void Save()
        {
            Debug.WriteLine($"{DateTime.Now} Saving");
            Entry today = new Entry
            {
                Date = DateTime.Now,
                LoggedTime = GetCurrentElapsed()
            };

            Stopwatch stopwatch = Stopwatch.StartNew();
            Storage.SaveEntry(today);
            stopwatch.Stop();
            Debug.WriteLine($"Save took {stopwatch.ElapsedMilliseconds} ms");
        }

        private void linkLabel_saveFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Storage.DataDirectory);
        }

        private void button_manuallyAdd_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox_manuallyAdd.Text, out int minutes))
            {
                _unlockedTimeElapsed += TimeSpan.FromMinutes(minutes);
                DisplayElapsedTime();
                Save();
            }
        }
    }
}
