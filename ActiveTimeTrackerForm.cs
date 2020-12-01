using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;

namespace ActiveTimeTracker
{
    public partial class ActiveTimeTrackerForm : Form
    {
        public ActiveTimeTrackerForm()
        {
            InitializeComponent();

            TryLoadingToday();
            StartDateRolloverTimer();
            StartTrackingElapsed();
            StartAutoSave();
        }

        private TimeSpan _unlockedTimeElapsed = TimeSpan.Zero;
        private DateTime _lastUnlock = DateTime.Now;
        private Timer _alwaysTimer;
        private Timer _saveTimer;
        private AbsoluteTimer.AbsoluteTimer _dateRolloverTimer;
        private DateTime _dateTracked = DateTime.Now.Date;

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

        private void StartTrackingElapsed()
        {
            SystemEvents.SessionSwitch += SystemEventsOnSessionSwitch;
            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;

            _alwaysTimer = new Timer();
            _alwaysTimer.Interval = 1000;
            _alwaysTimer.Tick += _displayElapsed_Tick;
            _alwaysTimer.Start();
        }

        private void _displayElapsed_Tick(object sender, EventArgs e)
        {
            DisplayElapsedTime();
        }

        private void DisplayElapsedTime()
        {
            label_unlockedTime.Text = FormatTimeSpan(GetCurrentElapsed());
        }

        private void StartAutoSave()
        {
            _saveTimer = new Timer();
            _saveTimer.Interval = 30000;
            _saveTimer.Tick += _saveTimer_Tick; ;
            _saveTimer.Start();
        }

        private void StopAutoSave()
        {
            _saveTimer?.Stop();
            _saveTimer?.Dispose();
            _saveTimer = null;
        }

        private void _saveTimer_Tick(object sender, EventArgs e)
        {
            Save();
        }

        private void StartDateRolloverTimer()
        {
            DateTime rolloverTime = DateTime.Now.AddDays(1).Date;
            Debug.WriteLine($"StartDateRolloverTimer scheduling reset at {rolloverTime}");
            _dateRolloverTimer?.Dispose();
            _dateRolloverTimer = new AbsoluteTimer.AbsoluteTimer(rolloverTime, DateRollover, null);
        }

        private void DateRollover(object state)
        {
            Debug.WriteLine($"{DateTime.Now} DateRollover");
            // Leave whatever was saved last - it's already past midnight, so the wrong day would be saved now
            // Reset counters
            _unlockedTimeElapsed = TimeSpan.Zero;
            _lastUnlock = DateTime.Now;
            _dateTracked = DateTime.Now.Date;
            StartDateRolloverTimer();
        }

        private static string FormatTimeSpan(TimeSpan elapsed)
        {
            return elapsed.ToString("hh\\:mm\\:ss");
        }

        private void SystemEventsOnSessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            Debug.WriteLine($"{DateTime.Now} SessionSwitch: {e.Reason}");
            switch (e.Reason)
            {
                case SessionSwitchReason.SessionLock:
                    OnUserDisconnected();
                    break;
                case SessionSwitchReason.SessionUnlock:
                    OnUserConnected();
                    break;
                default:
                    break;
            }
        }

        private void OnUserConnected()
        {
            _lastUnlock = DateTime.Now;
            Debug.WriteLine($"{DateTime.Now} OnUserConnected: Connected at {_lastUnlock}");
            StartAutoSave();
        }

        private void OnUserDisconnected()
        {
            TimeSpan currentElapsed = DateTime.Now - _lastUnlock;
            Debug.WriteLine($"{DateTime.Now} OnUserDisconnected: Disconnecting after {currentElapsed}");
            _unlockedTimeElapsed += currentElapsed;
            StopAutoSave();
        }

        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            Debug.WriteLine($"{DateTime.Now} PowerModeChanged: {e.Mode}");
            switch (e.Mode)
            {
                case PowerModes.Suspend:
                    OnUserDisconnected();
                    break;
                case PowerModes.Resume:
                    OnUserConnected();
                    break;
                default:
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
            DateTime now = DateTime.Now;

            if (_dateTracked != now.Date)
            {
                Debug.WriteLine($"{DateTime.Now} Not saving, date rollover overdue");
                return;
            }

            Debug.WriteLine($"{DateTime.Now} Saving");

            Entry today = new Entry
            {
                Date = now,
                LoggedTime = GetCurrentElapsed()
            };

            Stopwatch stopwatch = Stopwatch.StartNew();
            Storage.SaveEntry(today);
            stopwatch.Stop();
            Debug.WriteLine($"Save took {stopwatch.ElapsedMilliseconds} ms");
        }

        private void button_manuallyAdd_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox_manuallyAdd.Text, out int minutes))
            {
                _unlockedTimeElapsed += TimeSpan.FromMinutes(minutes);
                DisplayElapsedTime();
                Save();
                textBox_manuallyAdd.Text = string.Empty;
            }
        }

        private void linkLabel_saveFolder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Storage.DataDirectory);
        }
    }
}
