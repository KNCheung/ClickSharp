using WindowsInput;
using WindowsInput.Native;
using Gma.System.MouseKeyHook;
using System.Windows.Forms;
using System;

namespace ClickSharp
{
    class Handler
    {
        public static readonly Handler instance = new Handler();
        
        private IKeyboardMouseEvents hook;
        private InputSimulator simulator;

        public string Touch()
        {
            return "あっ!";
        }
        
        private Handler()
        {
            hook = Hook.GlobalEvents();
            simulator = new InputSimulator();
            Subscribe();
        }

        public void Subscribe()
        {
            hook.MouseWheelExt += VolumnControler;
            hook.MouseDownExt += MediaSwitcher;
            hook.MouseWheelExt += SwitchDesktop;
        }

        private void SwitchDesktop(object sender, MouseEventExtArgs e)
        {
            if (e.Y > (SystemInformation.PrimaryMonitorSize.Height - 5))
            {
                if (e.Delta > 0)
                    simulator.Keyboard.ModifiedKeyStroke(
                        new[] { VirtualKeyCode.CONTROL, VirtualKeyCode.LWIN },
                        VirtualKeyCode.LEFT);
                else
                    simulator.Keyboard.ModifiedKeyStroke(
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
                    simulator.Keyboard.KeyPress(VirtualKeyCode.MEDIA_NEXT_TRACK);
                    e.Handled = true;
                }
                else if (e.Button == MouseButtons.XButton2)
                {
                    simulator.Keyboard.KeyPress(VirtualKeyCode.MEDIA_PREV_TRACK);
                    e.Handled = true;
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                        simulator.Keyboard.KeyPress(VirtualKeyCode.MEDIA_PLAY_PAUSE);
                    else
                        simulator.Keyboard.KeyPress(VirtualKeyCode.VOLUME_MUTE);
                    e.Handled = true;
                }
            }
        }

        private DateTime lastTrackSwitchTime = DateTime.Now.AddSeconds(1);
        private void VolumnControler(object sender, MouseEventExtArgs e)
        {
            if ((e.X <= 2) && (e.Y <= 2))
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    if (DateTime.Now > lastTrackSwitchTime)
                    {
                        if (e.Delta > 0)
                            simulator.Keyboard.KeyPress(VirtualKeyCode.MEDIA_PREV_TRACK);
                        else
                            simulator.Keyboard.KeyPress(VirtualKeyCode.MEDIA_NEXT_TRACK);
                        lastTrackSwitchTime = DateTime.Now.AddSeconds(1);  // Prevent press too fast
                        e.Handled = true;                        
                    }
                }
                else
                {
                    if (e.Delta > 0)
                        simulator.Keyboard.KeyPress(VirtualKeyCode.VOLUME_UP);
                    else
                        simulator.Keyboard.KeyPress(VirtualKeyCode.VOLUME_DOWN);
                    e.Handled = true;
                }
            }
        }
        
    }
}
