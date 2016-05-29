using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biometria_Projekt.KeystrokeClasses
{
    public enum KeyStatus { KeyUp, KeyDown}
    class ClickStatistics
    {
        public int Asci { get; set; }
        public int Time { get; set; }
        public KeyStatus Status { get; set; }
        public int CountOfClick { get; set; }

        public ClickStatistics(int asci, int time, KeyStatus status, int countOfClick)
        {
            Asci = asci;
            Time = time;
            Status = status;
            CountOfClick = countOfClick;
        }

        public ClickStatistics(int asci, int time, KeyStatus status)
        {
            Asci = asci;
            Time = time;
            Status = status;
        }

    }
}
