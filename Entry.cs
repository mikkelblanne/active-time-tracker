using System;

namespace ActiveTimeTracker
{
    public class Entry
    {
        public DateTime Date { get; set; }
        public TimeSpan LoggedTime { get; set; }
    }
}
