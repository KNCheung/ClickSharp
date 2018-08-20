using WindowsInput;
using WindowsInput.Native;
using Gma.System.MouseKeyHook;
using System.Windows.Forms;
using System;
using System.Configuration;
using System.Drawing;

namespace ClickSharp
{
    internal static class MouseHandler
    {
        
        private static readonly IKeyboardMouseEvents _hook;
        private static readonly int _zoneWidth;

        private static EventHandler<MouseEventExtArgs> _mouseKeyEventHandlers;
        private static EventHandler<MouseEventExtArgs> _mouseWheelEventHandlers;

        public static bool Left;
        public static bool Right;
        public static bool Top;
        public static bool Bottom;
        public static bool TopLeft => (Top && Left);
        public static bool BottomLeft => (Bottom && Left);
        public static bool TopRight => (Top && Right);
        public static bool BottomRight => (Bottom && Right);

        static MouseHandler()
        {
            try
            {
                _zoneWidth = int.Parse(ConfigurationManager.AppSettings["ZoneWidth"]);
            }
            catch (Exception)
            {
                _zoneWidth = 5;
            }
            _hook = Hook.GlobalEvents();
            Subscribe();
        }

        private static void Subscribe()
        {
            _hook.MouseDownExt += KeyDown;
            _hook.MouseWheelExt += Wheel;
        }

        public static string Touch()
        {
            return MagicWords.Greeting;
        }

        private static void DetectLocation(Point point)
        {
            Point realMonitorSize = MonitorInfo.RealPrimaryMonitorSize;
            Left = (point.X <= _zoneWidth);
            Right = (point.X >= realMonitorSize.X - _zoneWidth);
            Top = (point.Y <= _zoneWidth);
            Bottom = (point.Y >= realMonitorSize.Y - _zoneWidth);
        }

        private static void KeyDown(object sender, MouseEventExtArgs e)
        {
            DetectLocation(e.Location);
            _mouseKeyEventHandlers?.Invoke(sender, e);
        }

        private static void Wheel(object sender, MouseEventExtArgs e)
        {
            DetectLocation(e.Location);
            _mouseWheelEventHandlers?.Invoke(sender, e);
        }

        public static void SubscribeKey(EventHandler<MouseEventExtArgs> action)
        {
            _mouseKeyEventHandlers += action;
        }

        public static void SubscribeWheel(EventHandler<MouseEventExtArgs> action)
        {
            _mouseWheelEventHandlers += action;
        }
    }
}
