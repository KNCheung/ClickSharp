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

    internal class Program
    {
        private static void Main(string[] args)
        {
            TrayIcon.Balloon(MouseHandler.Touch(), 
                1000,
                MagicWords.Program_Title, 
                ToolTipIcon.Info);
            MouseHandler.SubscribeKey(Handlers.MediaSwitcher);
            MouseHandler.SubscribeWheel(Handlers.SwitchDesktop);
            MouseHandler.SubscribeWheel(Handlers.VolumnControler);
            Application.Run();
        }
    }
}
