using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biometria_Projekt.KeystrokeClasses
{
    public class KeyStatistics
    {
        public List<int> Asci { get; set; }
        public int TotalTime { get; set; }
        public int CountOfClick { get; set; }

        public double AverageTime
        {
            get
            {
                if (CountOfClick != 0)
                {
                    return TotalTime/CountOfClick;
                }
                return 0;
            }
        }
    }
}
