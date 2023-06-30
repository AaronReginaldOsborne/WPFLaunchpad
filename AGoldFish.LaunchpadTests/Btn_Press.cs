using AGoldFish.Launchpad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGoldFish.LaunchpadTests
{
    class Btn_Press
    {
        private LaunchpadDevice mLaunchpadDevice;
        private LaunchpadButton mLaunchpadButton;

        public Btn_Press(LaunchpadDevice device)
        {
            mLaunchpadDevice = device;

            mLaunchpadDevice.DoubleBuffered = false;
            mLaunchpadDevice.ButtonPressed += mLaunchpadDevice_ButtonPressed;

            mLaunchpadDevice.GetButton(ToolbarButton.Session).SetBrightness(ButtonBrightness.Full, ButtonBrightness.Full);

            //for (int y = 0; y < 4; y++)
            //{
            //    for (int x = 0; x < 4; x++)
            //    {
            //        mLaunchpadDevice[x, y].SetBrightness((ButtonBrightness)x, (ButtonBrightness)y);
            //    }
            //}
        }

        private void mLaunchpadDevice_ButtonPressed(object sender, ButtonPressEventArgs e)
        {
            if (e.Type == ButtonType.Grid)
            {
                mLaunchpadButton = mLaunchpadDevice[e.X, e.Y];
                if (mLaunchpadButton.RedBrightness == ButtonBrightness.Off && mLaunchpadButton.GreenBrightness == ButtonBrightness.Off)
                {
                    mLaunchpadButton.SetBrightness(ButtonBrightness.Full, ButtonBrightness.Full);
                    SpecialCases(e.Y,e.X);
                }
                else
                    mLaunchpadButton.SetBrightness(ButtonBrightness.Off, ButtonBrightness.Off);

                

                //if (button.RedBrightness == ButtonBrightness.Off && button.GreenBrightness == ButtonBrightness.Off)
                //    button.SetBrightness(ButtonBrightness.Full, ButtonBrightness.Off);
                //else if (button.RedBrightness == ButtonBrightness.Full && button.GreenBrightness == ButtonBrightness.Off)
                //    button.SetBrightness(ButtonBrightness.Off, ButtonBrightness.Full);
                //else if (button.RedBrightness == ButtonBrightness.Off && button.GreenBrightness == ButtonBrightness.Full)
                //    button.SetBrightness(ButtonBrightness.Full, ButtonBrightness.Full);
                //else
                //    button.SetBrightness(ButtonBrightness.Off, ButtonBrightness.Off);

            }
            else if (e.Type == ButtonType.Toolbar)
            {
                if (e.ToolbarButton == ToolbarButton.Session)
                {
                    for (int y = 0; y < 8; y++)
                        for (int x = 0; x < 8; x++)
                            mLaunchpadDevice[x, y].TurnOffLight();
                }
            }
        }

        private void SpecialCases( int x, int y)
        {
            //start at top left to bottom right
            if(x==0 && y == 0)
                mLaunchpadButton.SetBrightness(ButtonBrightness.Off, ButtonBrightness.Full);
            if (x == 0 && y == 8)
                mLaunchpadButton.SetBrightness(ButtonBrightness.Off, ButtonBrightness.Full);

        }

        public void Run()
        {
            while (true) ;
        }
    }
}
