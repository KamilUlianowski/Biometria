using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biometria_Projekt.Classes
{
    public static class Binaryzation
    {
        public static int[] GetLUTForBinaryzation(int treshold)
        {
            var LUT = new int[256];

            for (var i = 0; i <= 255; i++)
            {
                LUT[i] = (i < treshold) ? 0 : 255;
            }

            return LUT;
        }



        public static int GetOtsuTreshold(int[] histData, int total)
        {
            float sum = 0;
            for (int t = 0; t < 256; t++) sum += t * histData[t];

            float sumB = 0;
            var wB = 0;
            var wF = 0;

            float varMax = 0;
            var threshold = 0;

            for (int t = 0; t < 256; t++)
            {
                wB += histData[t];
                if (wB == 0) continue;

                wF = total - wB;
                if (wF == 0) break;

                sumB += (float)(t * histData[t]);

                var mB = sumB / wB;
                var mF = (sum - sumB) / wF;

                var varBetween = (float)wB * (float)wF * (mB - mF) * (mB - mF);

                if (varBetween > varMax)
                {
                    varMax = varBetween;
                    threshold = t;
                }
            }

            return threshold;
        }
    }
}
