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
        public void maskTest()
        {
            var exampleTable = new int[3, 3]
            {
                {1, 0, 2},
                {1, 0, 5},
                {2, 0, 1}
            };
            // -1 + 2 -2 + 2 - 2 + 1
            var mask = new int[3, 3]
            {
                {-1, 0, 1},
                {-2, 0, 2},
                {-1, 0, 1}
            };
            var sum = 0;
            var expected = 8;
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