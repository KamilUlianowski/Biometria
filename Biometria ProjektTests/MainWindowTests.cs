using Microsoft.VisualStudio.TestTools.UnitTesting;
using Biometria_Projekt;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Biometria_Projekt.Classes;

namespace Biometria_Projekt.Tests
{
    [TestClass()]
    public class MainWindowTests
    {
        [TestMethod()]
        public void addTest()
        {
            var exptected = 9;
            var actual = MainWindow.add(5, 4); 
            Assert.AreEqual(exptected,actual);
        }

        [TestMethod]
        public void AverageBrithtnessTest()
        {
            var exampleTable = new int[5, 5]
            {
                {100, 200, 50,40,30},
                {40, 20, 50, 90, 120},
                {30, 50, 90, 50,120},
                {20, 60, 120,230,110},
                {60, 70, 150,70,110}
            };
        }

        [TestMethod()]
        public void maskTest()
        {
            var exampleTable = new int[3, 3]
            {
                {0, 0, 0},
                {0, 1, 1},
                {0, 1, 2}
            };
            var mask = new int[3, 3]
            {
                {4, 0, 0},
                {0, 0, 0},
                {0, 0, -4}
            };
            var sum = 0;
            var expected = -8;
            for (int x = 0; x <= 2; x++)
            {
                for (int y = 0; y <= 2; y++)
                {
                    int x2 = 0;
                    int y2 = 0;
                    if (x == 1) x2 = 1;
                    if (x < 1) x2 = 0;
                    if (x > 1) x2 = 2;
                    if (y == 1) y2 = 1;
                    if (y < 1) y2 = 0;
                    if (y > 1) y2 = 2;

                    sum += mask[x2, y2]*exampleTable[x, y];
                }
            }
            Assert.AreEqual(expected,sum);
            
        }
    }
}