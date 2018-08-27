using System;
using System.Windows.Forms;

namespace ClickSharp
{
    static class TrayIcon
    {
        private static readonly NotifyIcon NotifyIcon;
        private static readonly ContextMenu ContextMenu;
        private static MenuItem _menu;
        static TrayIcon()
        {
            NotifyIcon = new NotifyIcon();
            ContextMenu = new ContextMenu();

            CreateMenu();
            CreateNotifyIcon();
        }

        private static void Quit(object sender, EventArgs e)
        {
            NotifyIcon.Dispose();
            Application.Exit();
        }

        private static void Restart(object sender, EventArgs e)
        {
            NotifyIcon.Dispose();
            Application.Restart();
        }

        private static void CreateMenu()
        {
            _menu = new MenuItem
            {
                Index = 0,
                Text = MagicWords.Tray_Restart,
                DefaultItem = true
            };
            _menu.Click += Restart;

            ContextMenu.MenuItems.Add(_menu);
            
            _menu = new MenuItem
            {
                Index = 1,
                Text = MagicWords.Tray_Menu_Exit
            };
            _menu.Click += Quit;

            ContextMenu.MenuItems.Add(_menu);
        }

        private static void CreateNotifyIcon()
        {
            NotifyIcon.Icon = Resource.MainIcon;
            NotifyIcon.Text = MagicWords.Tray_Text;
            NotifyIcon.ContextMenu = ContextMenu;
            NotifyIcon.DoubleClick += Restart;
            NotifyIcon.Visible = true;
        }

        /// <summary>
        /// 弹出气泡消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="timeout">停留毫秒数（建议）</param>
        /// <param name="title">标题</param>
        /// <param name="icon">图标</param>
        public static void Balloon(string message, int timeout = 1000, string title = "Hello", ToolTipIcon icon = ToolTipIcon.None)
        {
            NotifyIcon.ShowBalloonTip(timeout, title, message, icon);
        }
    }
}
