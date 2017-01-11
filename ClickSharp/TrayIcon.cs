using System;
using System.Windows.Forms;

namespace ClickSharp
{
    static class TrayIcon
    {
        private static NotifyIcon notifyIcon;
        private static ContextMenu contextMenu;
        private static MenuItem menu;
        static TrayIcon()
        {
            CreateMenu();
            CreateNotifyIcon();
        }

        static private void Quit(object sender, EventArgs e)
        {
            notifyIcon.Dispose();
            Application.Exit();
        }

        static private void CreateMenu()
        {
            menu = new MenuItem();
            menu.Index = 0;
            menu.Text = "じゃね(&E)";
            menu.Click += Quit;

            contextMenu = new ContextMenu();
            contextMenu.MenuItems.Add(menu);
        }

        private static void CreateNotifyIcon()
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = Resource.MainIcon;
            notifyIcon.Text = "Click♯";
            notifyIcon.ContextMenu = contextMenu;
            notifyIcon.DoubleClick += Quit;
            notifyIcon.Visible = true;
        }
        
        /// <summary>
        /// 弹出气泡消息
        /// </summary>
        /// <param name="timeout">停留毫秒数（建议）</param>
        /// <param name="title">标题</param>
        /// <param name="message">消息</param>
        /// <param name="icon">图标</param>
        public static void Balloon(int timeout, string title, string message, ToolTipIcon icon)
        {
            notifyIcon.ShowBalloonTip(timeout, title, message, icon);
        }
    }
}
