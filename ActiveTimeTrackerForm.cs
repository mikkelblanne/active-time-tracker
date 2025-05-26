using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using log4net;
using Microsoft.Win32;

namespace ActiveTimeTracker
{
    public partial class ActiveTimeTrackerForm : Form
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ActiveTimeTrackerForm));

        public ActiveTimeTrackerForm()
        {
            InitializeComponent();
            log.Info($"Starting up version {Application.ProductVersion}");

            TryLoadingToday();
            StartDateRolloverTimer();
            StartTrackingElapsed();
            StartAutoSave();
            FormClosing += ActiveTimeTrackerForm_FormClosing;
        }

        private void ActiveTimeTrackerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsAutoSaveEnabled())
            {
                Save();
            }

            log.Info("Exiting");
        }

        private TimeSpan _unlockedTimeElapsed = TimeSpan.Zero;
        private DateTime _lastUnlock = DateTime.Now;
        private Timer _alwaysTimer;
        private Timer _saveTimer;
        private AbsoluteTimer.AbsoluteTimer _dateRolloverTimer;
        private DateTime _dateTracked = DateTime.Now.Date;
        private bool _isUnlocked = true;

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
            StopAutoSave(); // Avoid potential multiple timers in case of multiple events
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

        private bool IsAutoSaveEnabled()
        {
            return _saveTimer != null;
        }

        private void _saveTimer_Tick(object sender, EventArgs e)
        {
            Save();
        }

        private void StartDateRolloverTimer()
        {
            DateTime rolloverTime = DateTime.Now.AddDays(1).Date;
            log.Info($"StartDateRolloverTimer scheduling reset at {rolloverTime}");
            _dateRolloverTimer?.Dispose();
            _dateRolloverTimer = new AbsoluteTimer.AbsoluteTimer(rolloverTime, DateRollover, null);
        }

        private void DateRollover(object state)
        {
            log.Info($"DateRollover");
            // Leave whatever was saved last - it's already past midnight, so the wrong day would be saved now
            // Reset counters
            _unlockedTimeElapsed = TimeSpan.Zero;
            _lastUnlock = DateTime.Now;
            _dateTracked = DateTime.Now.Date;
            StartDateRolloverTimer();
        }

        private static string FormatTimeSpan(TimeSpan elapsed)
        {
            return string.Format("{0:#,##0}:{1:mm}:{1:ss}", Math.Truncate(elapsed.TotalHours), elapsed);
        }

        private void SystemEventsOnSessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            log.Info($"SessionSwitch: {e.Reason}");
            switch (e.Reason)
            {
                case SessionSwitchReason.SessionLock:
                    if (this._isUnlocked)
                    {
                        OnUserDisconnected();
                    }
                    break;
                case SessionSwitchReason.SessionUnlock:
                    if (!this._isUnlocked)
                    {
                        OnUserConnected();
                    }
                    break;
                default:
                    break;
            }
        }

        private void OnUserConnected()
        {
            this._isUnlocked = true;
            _lastUnlock = DateTime.Now;
            log.Info($"OnUserConnected: Connected at {_lastUnlock}");
            StartAutoSave();
        }

        private void OnUserDisconnected()
        {
            this._isUnlocked = false;
            TimeSpan currentElapsed = DateTime.Now - _lastUnlock;
            log.Info($"OnUserDisconnected: Disconnecting after {FormatTimeSpan(currentElapsed)} (previous total: {FormatTimeSpan(_unlockedTimeElapsed)})");
            StopAutoSave();
            Save();
            // Save() calculates elapsed time, so only increment afterwards
            _unlockedTimeElapsed += currentElapsed;
        }

        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            log.Info($"PowerModeChanged: {e.Mode}");
                switch (e.Mode)
                {
                    case PowerModes.Suspend:
                        if (_isUnlocked)
                        {
                            OnUserDisconnected();
                        }
                        break;
                    case PowerModes.Resume:
                        if (!_isUnlocked)
                        {
                            OnUserConnected();
                        }
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
                log.Info($"Not saving, date rollover overdue");
                return;
            }

            Entry today = new Entry
            {
                Date = now,
                LoggedTime = GetCurrentElapsed()
            };

            Stopwatch stopwatch = Stopwatch.StartNew();
            Storage.SaveEntry(today);
            stopwatch.Stop();
            log.Debug($"LoggedTime {FormatTimeSpan(today.LoggedTime)} saved in {stopwatch.ElapsedMilliseconds} ms");
        }

        private void button_manuallyAdd_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox_manuallyAdd.Text, out int minutes))
            {
                log.Info($"Manually adding minutes: {minutes}");
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
