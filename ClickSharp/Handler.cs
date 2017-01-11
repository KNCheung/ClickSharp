using WindowsInput;
using WindowsInput.Native;
using Gma.System.MouseKeyHook;
using System.Windows.Forms;

namespace ClickSharp
{
    class Handler
    {
        public static readonly Handler instance = new Handler();
        
        private IKeyboardMouseEvents hook;
        private InputSimulator sim;

        public string Touch()
        {
            return "あっ!";
        }
        
        private Handler()
        {
            hook = Hook.GlobalEvents();
            sim = new InputSimulator();
            Subscribe();
        }

        public void Subscribe()
        {
            hook.MouseWheelExt += VolumnControler;
            hook.MouseDownExt += MediaSwitcher;
            hook.MouseWheelExt += SwitchDesktop;
            hook.KeyPress += GetSpecialKeyStatus;
        }

        private void SwitchDesktop(object sender, MouseEventExtArgs e)
        {
            if (e.Y > (SystemInformation.PrimaryMonitorSize.Height - 5))
            {
                if (e.Delta > 0)
                    sim.Keyboard.ModifiedKeyStroke(
                        new[] { VirtualKeyCode.CONTROL, VirtualKeyCode.LWIN },
                        VirtualKeyCode.LEFT);
                else
                    sim.Keyboard.ModifiedKeyStroke(
                        new[] { VirtualKeyCode.CONTROL, VirtualKeyCode.LWIN },
                        VirtualKeyCode.RIGHT);
                e.Handled = true;
            }
        }

        private void MediaSwitcher(object sender, MouseEventExtArgs e)
        {
            if ((e.X <= 2) && (e.Y <= 2))
            {
                if (e.Button == MouseButtons.XButton1)
                {
                    sim.Keyboard.KeyPress(VirtualKeyCode.MEDIA_NEXT_TRACK);
                    e.Handled = true;
                }
                else if (e.Button == MouseButtons.XButton2)
                {
                    sim.Keyboard.KeyPress(VirtualKeyCode.MEDIA_PREV_TRACK);
                    e.Handled = true;
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                        sim.Keyboard.KeyPress(VirtualKeyCode.MEDIA_PLAY_PAUSE);
                    else
                        sim.Keyboard.KeyPress(VirtualKeyCode.VOLUME_MUTE);
                    e.Handled = true;
                }
            }
        }
        
        private void VolumnControler(object sender, MouseEventExtArgs e)
        {
            if ((e.X <= 2) && (e.Y <= 2))
            {
                if (e.Delta > 0)
                    sim.Keyboard.KeyPress(VirtualKeyCode.VOLUME_UP);
                else
                    sim.Keyboard.KeyPress(VirtualKeyCode.VOLUME_DOWN);
                e.Handled = true;
            }
        }

        private void GetSpecialKeyStatus(object sender, KeyPressEventArgs e)
        {
        }
    }
}
