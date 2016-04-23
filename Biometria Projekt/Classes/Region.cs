using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biometria_Projekt.Classes
{
    class Region   
    {
        public int AvgBrightness { get; set; }
        public double Variance { get; set; }

        public Region(int avgBrightness,double variance)
        {
            AvgBrightness = avgBrightness;
            Variance = variance;
        }
    }
}
