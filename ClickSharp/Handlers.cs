using System;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;
using Gma.System.MouseKeyHook;

namespace ClickSharp
{
    internal static class Handlers
    {
        private static readonly InputSimulator _simulator;


        static Handlers()
        {
            _simulator = new InputSimulator();
        }
        
        public static void SwitchDesktop(object sender, MouseEventExtArgs e)
        {
            if (!MouseHandler.Bottom) return;
            _simulator.Keyboard.ModifiedKeyStroke(
                new[] {VirtualKeyCode.CONTROL, VirtualKeyCode.LWIN},
                e.Delta > 0 ? VirtualKeyCode.LEFT : VirtualKeyCode.RIGHT);
            e.Handled = true;
        }

        public static void MediaSwitcher(object sender, MouseEventExtArgs e)
        {
            if (!MouseHandler.TopLeft) return;
            switch (e.Button)
            {
                case MouseButtons.XButton1:
                    _simulator.Keyboard.KeyPress(VirtualKeyCode.MEDIA_NEXT_TRACK);
                    e.Handled = true;
                    break;
                case MouseButtons.XButton2:
                    _simulator.Keyboard.KeyPress(VirtualKeyCode.MEDIA_PREV_TRACK);
                    e.Handled = true;
                    break;
                case MouseButtons.Middle:
                    _simulator.Keyboard.KeyPress((Control.ModifierKeys & Keys.Control) == Keys.Control
                        ? VirtualKeyCode.MEDIA_PLAY_PAUSE
                        : VirtualKeyCode.VOLUME_MUTE);
                    e.Handled = true;
                    break;
                case MouseButtons.Left:
                    break;
                case MouseButtons.None:
                    break;
                case MouseButtons.Right:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static DateTime _lastTrackSwitchTime = DateTime.Now.AddSeconds(1);
        public static void VolumnControler(object sender, MouseEventExtArgs e)
        {
            if (!MouseHandler.TopLeft) return;
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
            {
                if (DateTime.Now > _lastTrackSwitchTime)
                {
                    if (e.Delta > 0)
                        _simulator.Keyboard.KeyPress(VirtualKeyCode.MEDIA_PREV_TRACK);
                    else
                        _simulator.Keyboard.KeyPress(VirtualKeyCode.MEDIA_NEXT_TRACK);
                    _lastTrackSwitchTime = DateTime.Now.AddSeconds(1);  // Prevent press too fast
                    e.Handled = true;
                }
            }
            else
            {
                _simulator.Keyboard.KeyPress(e.Delta > 0 ? VirtualKeyCode.VOLUME_UP : VirtualKeyCode.VOLUME_DOWN);
                e.Handled = true;
            }
        }
    }
}
