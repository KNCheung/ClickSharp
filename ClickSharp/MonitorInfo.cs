using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ClickSharp
{
    class MonitorInfo
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr ptr);
        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        [DllImport("user32.dll", EntryPoint="ReleaseDC")]
        static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);

        public static Point RealPrimaryMonitorSize
        {
            get
            {
                IntPtr hdc = GetDC(IntPtr.Zero);
                Point ret = new Point();
                ret.X = GetDeviceCaps(hdc, 118);
                ret.Y = GetDeviceCaps(hdc, 117);
                ReleaseDC(IntPtr.Zero, hdc);
                return ret;
            }
        }
    }
}
