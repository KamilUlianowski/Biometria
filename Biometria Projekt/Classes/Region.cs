using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biometria_Projekt.Classes
{
    class Region   
    {
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int AvgBrightnessRed { get; set; }
        public int AvgBrightnessGreen { get; set; }
        public int AvgBrightnessBlue { get; set; }
        public double VarianceRed { get; set; }
        public double VarianceGreen { get; set; }
        public double VarianceBlue { get; set; }

        public Region(int startX,int startY)
        {
            StartX = startX;
            StartY = startY;
        }
    }
}
