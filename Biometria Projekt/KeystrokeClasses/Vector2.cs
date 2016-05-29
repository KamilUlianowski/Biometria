using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CsvHelper.Configuration;

namespace Biometria_Projekt.Classes
{
    public sealed class Vector2 : CsvClassMap<Vector2>

    {
        public int UserId { get; set; }
        private int TotalKeyDownTime;
        private int TotalBreakTime;
        private int TotalKeyUpSpaceDownTime;
        private int TotalSpaceUpKeyDownTime;
        public int AverageKeyDownTime
        {
            get { return TotalKeyDownTime / count; }
        }

        public int AverageBreakTime
        {
            get { return TotalBreakTime / count; }
        }

        public int AverageKeyUpSpaceDownTime
        {
            get { return TotalKeyUpSpaceDownTime / count; }
        }

        public int AverageSpaceUpKeyDownTime
        {
            get { return TotalSpaceUpKeyDownTime / count; }
        }
        private int count;

        public Vector2(int userId, int totalKeyDownTime, int totalBreakTime,
            int totalKeyUpSpaceDownTime, int totalSpaceUpKeyDownTime)
        {
            this.UserId = userId;
            this.TotalBreakTime = totalBreakTime;
            this.TotalKeyDownTime = totalKeyDownTime;
            this.TotalKeyUpSpaceDownTime = totalKeyUpSpaceDownTime;
            this.TotalSpaceUpKeyDownTime = totalSpaceUpKeyDownTime;
            Map(x => x.UserId);
            Map(x => x.AverageKeyDownTime);
            Map(x => x.AverageBreakTime);
            Map(x => x.AverageKeyUpSpaceDownTime);
            Map(x => x.AverageSpaceUpKeyDownTime);
            count = 1;
        }

        public void Sum(Vector2 vector2)
        {
            if (vector2.TotalBreakTime == 0 || vector2.TotalKeyDownTime == 0 || vector2.TotalKeyUpSpaceDownTime == 0
                || vector2.TotalSpaceUpKeyDownTime == 0) return;
            TotalKeyDownTime += vector2.TotalKeyDownTime;
            TotalBreakTime += vector2.TotalBreakTime;
            TotalKeyUpSpaceDownTime +=  vector2.TotalKeyUpSpaceDownTime;
            TotalSpaceUpKeyDownTime += vector2.TotalSpaceUpKeyDownTime;
            count ++;
        }

    }
}
