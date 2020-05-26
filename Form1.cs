using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Win32;

namespace StopWatch
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            StartAutoCounter();
        }

        private int _manualSecondsElapsed;
        private bool _manualEnabled;
        private readonly Timer _manualTimer = new Timer();

        private void button_toggle_Click(object sender, EventArgs e)
        {
            if (_manualEnabled)
            {
                _manualEnabled = false;
                _manualTimer.Stop();
                _manualTimer.Tick -= ManualTimerOnTick;
                button_manualTime.Text = "Start";
            }
            else
            {
                _manualEnabled = true;
                _manualTimer.Interval = 1000;
                _manualTimer.Tick += ManualTimerOnTick;
                _manualTimer.Start();
                button_manualTime.Text = "Stop";
            }
        }

        private void ManualTimerOnTick(object sender, EventArgs e)
        {
            _manualSecondsElapsed += 1;
            TimeSpan span = TimeSpan.FromSeconds(_manualSecondsElapsed);
            label_manualTime.Text = span.ToString();
        }

        private TimeSpan _unlockedTimeElapsed = TimeSpan.Zero;
        private DateTime _lastUnlock;
        private readonly Timer _alwaysTimer = new Timer();

        private void StartAutoCounter()
        {
            SystemEvents.SessionSwitch += SystemEventsOnSessionSwitch;

            _lastUnlock = DateTime.Now;
            _alwaysTimer.Interval = 1000;
            _alwaysTimer.Tick += _autoTimer_Tick;
            _alwaysTimer.Start();
        }

        private void _autoTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = _unlockedTimeElapsed;
            elapsed += DateTime.Now - _lastUnlock;
            label_unlockedTime.Text = elapsed.ToString("hh\\:mm\\:ss");
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
    }
}
