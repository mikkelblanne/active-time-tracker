using System;
using System.Windows.Forms;

[assembly: log4net.Config.XmlConfigurator()]

namespace ActiveTimeTracker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ActiveTimeTrackerForm());
        }
    }
}
