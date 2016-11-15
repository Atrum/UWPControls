using System;

namespace AtrumSoft.UWP.Controls.Tools
{
    public class CircleUtils
    {
        public static double GetAngleFor(double value)
        {
            return 360 * value * Math.PI / 180;
        }
    }
}