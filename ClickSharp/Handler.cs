using WindowsInput;
using WindowsInput.Native;
using Gma.System.MouseKeyHook;
using System.Windows.Forms;
using System;

namespace ClickSharp
{
    class Handler
    {
        public static readonly Handler Instance = new Handler();
        
        private readonly IKeyboardMouseEvents _hook;
        private readonly InputSimulator _simulator;

        public string Touch()
        {
            return "あっ!";
        }
        
        private Handler()
        {
            _hook = Hook.GlobalEvents();
            _simulator = new InputSimulator();
            Subscribe();
        }

        public void Subscribe()
        {
            _hook.MouseWheelExt += VolumnControler;
            _hook.MouseDownExt += MediaSwitcher;
            _hook.MouseClick += PageRollerDown;
            _hook.MouseWheelExt += SwitchDesktop;
        }
        
        private void PageRollerDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.XButton1:
                    _simulator.Keyboard.KeyDown(VirtualKeyCode.NEXT);
                    break;
                case MouseButtons.XButton2:
                    _simulator.Keyboard.KeyDown(VirtualKeyCode.PRIOR);
                    break;
                case MouseButtons.Left:
                    break;
                case MouseButtons.None:
                    break;
                case MouseButtons.Right:
                    break;
                case MouseButtons.Middle:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void SwitchDesktop(object sender, MouseEventExtArgs e)
        {
            if (e.Y > (SystemInformation.PrimaryMonitorSize.Height - 5))
            {
                if (e.Delta > 0)
                    _simulator.Keyboard.ModifiedKeyStroke(
                        new[] { VirtualKeyCode.CONTROL, VirtualKeyCode.LWIN },
                        VirtualKeyCode.LEFT);
                else
                    _simulator.Keyboard.ModifiedKeyStroke(
                        new[] { VirtualKeyCode.CONTROL, VirtualKeyCode.LWIN },
                        VirtualKeyCode.RIGHT);
                e.Handled = true;
            }
        }

        private void MediaSwitcher(object sender, MouseEventExtArgs e)
        {
            if ((e.X > 2) || (e.Y > 2)) return;
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

        private DateTime _lastTrackSwitchTime = DateTime.Now.AddSeconds(1);
        private void VolumnControler(object sender, MouseEventExtArgs e)
        {
            if ((e.X <= 2) && (e.Y <= 2))
            {
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
                    if (e.Delta > 0)
                        _simulator.Keyboard.KeyPress(VirtualKeyCode.VOLUME_UP);
                    else
                        _simulator.Keyboard.KeyPress(VirtualKeyCode.VOLUME_DOWN);
                    e.Handled = true;
                }
            }
        }
        
    }
}
