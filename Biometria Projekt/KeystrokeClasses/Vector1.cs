using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Biometria_Projekt.KeystrokeClasses;
using CsvHelper.Configuration;

namespace Biometria_Projekt.Classes
{
    public class Vector1
    {
        public int UserId { get; set; }
        public List<KeyStatistics> Keys { get; set; }

        public Vector1(int userId, List<KeyStatistics> keys )
        {
            this.UserId = userId;
            this.Keys = keys;
        }

        public void SumTimes(Vector1 vector)
        {
            foreach (var item in vector.Keys)
            {
                var innerItem = Keys.First(x => x.Asci[0] == item.Asci[0]);
                if (innerItem != null)
                {
                    Keys[Keys.IndexOf(innerItem)].TotalTime += item.TotalTime;
                    Keys[Keys.IndexOf(innerItem)].CountOfClick += item.CountOfClick;
                }
            }
        } 
    }
}
