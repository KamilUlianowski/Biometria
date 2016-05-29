using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace Biometria_Projekt.Classes
{
    public class ObsoleteKeyStatistics
    {
        public List<int> Asci { get; set; }
        public int CountOfClick { get; set; }
        public double TotalTime { get; set; }

        public double AverageTime
        {
            get
            {
                if (CountOfClick != 0)
                    return TotalTime / CountOfClick;
                else return 0;
            }
        }

        public ObsoleteKeyStatistics(int totalTime)
        {
            this.CountOfClick = 1;
            this.TotalTime = totalTime;
        }

        public ObsoleteKeyStatistics()
        {

        }
    }
}
