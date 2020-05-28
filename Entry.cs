using System;

namespace WorkingClock
{
    public class Entry
    {
        public DateTime Date { get; set; }
        public TimeSpan LoggedTime { get; set; }
    }
}
