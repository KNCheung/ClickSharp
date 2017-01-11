using System;
using System.Windows.Forms;

namespace ClickSharp
{
//    public static class Log
//    {
//        static Log()
//        {
//            var config = new LoggingConfiguration();
//            var consoleTarget = new ColoredConsoleTarget();
//            consoleTarget.Layout = @"${level:uppercase=true} ${message}";
//            config.AddTarget("console", consoleTarget);
//            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Info, consoleTarget));
//            LogManager.Configuration = config;
//        }
        
//        public static Logger GetLogger()
//        {
//            return LogManager.GetCurrentClassLogger();
//        }
//    }

    class Program
    {
        static void Main(string[] args)
        {
            TrayIcon.Balloon(1000, "Hello", Handler.instance.Touch(), ToolTipIcon.Info);
            Application.Run();
        }
    }
}
