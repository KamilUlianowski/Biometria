using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biometria_Projekt.Classes
{
    public class Filters
    {
        public ImageProperties ImageProperties { get; set; }
        public static int[,] GetLowPassMask()
        {
            var mask = new int[3, 3]
            {
                {0, -1, 0},
                {-1, 4, -1},
                {0, -1, 0}
            };
            return mask;
        }

        public static int[,] GetDetectionCornersMask()
        {
            var mask = new int[3, 3]
            {
                {-1, 1, 1},
                {-1, -2, 1},
                {-1, 1, 1}
            };
            return mask;
        }

    }
}
