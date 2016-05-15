using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biometria_Projekt.Classes
{
    class Thinning
    {
        public static T[][] ArrayClone<T>(T[][] A)
        {
            return A.Select(a => a.ToArray()).ToArray();
        }

        public static bool[][] ZhangSuenThinning(bool[][] s)
        {
            var temp = ArrayClone(s); // make a deep copy to start.. 
            var count = 0;
            do // the missing iteration
            {
                count = step(1, temp, s);
                temp = ArrayClone(s); // ..and on each..
                count += step(2, temp, s);
                temp = ArrayClone(s); // ..call!
            } while (count > 0);

            var count2 = 0;

            return s;
        }

        private static int step(int stepNo, bool[][] temp, bool[][] s)
        {
            var count = 0;

            for (var a = 1; a < temp.Length - 1; a++)
            {
                for (var b = 1; b < temp[0].Length - 1; b++)
                {
                    if (SuenThinningAlg(a, b, temp, stepNo == 2))
                    {
                        // still changes happening?
                        if (s[a][b]) count++;
                        s[a][b] = false;
                    }
                }
            }
            return count;
        }

        private static bool SuenThinningAlg(int x, int y, bool[][] s, bool even)
        {
            var p2 = s[x][y - 1];
            var p3 = s[x + 1][y - 1];
            var p4 = s[x + 1][y];
            var p5 = s[x + 1][y + 1];
            var p6 = s[x][y + 1];
            var p7 = s[x - 1][y + 1];
            var p8 = s[x - 1][y];
            var p9 = s[x - 1][y - 1];


            var bp1 = NumberOfNonZeroNeighbors(x, y, s);
            if (bp1 >= 2 && bp1 <= 6) //2nd condition
            {
                if (NumberOfZeroToOneTransitionFromP9(x, y, s) == 1)
                {
                    if (even)
                    {
                        if (!(p2 && p4 && p8))
                        {
                            if (!(p2 && p6 && p8))
                            {
                                return true;
                            }
                        }
                    }
                    else
                    {
                        if (!(p2 && p4 && p6))
                        {
                            if (!(p4 && p6 && p8))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private static int NumberOfZeroToOneTransitionFromP9(int x, int y, bool[][] s)
        {
            var p2 = s[x][y - 1];
            var p3 = s[x + 1][y - 1];
            var p4 = s[x + 1][y];
            var p5 = s[x + 1][y + 1];
            var p6 = s[x][y + 1];
            var p7 = s[x - 1][y + 1];
            var p8 = s[x - 1][y];
            var p9 = s[x - 1][y - 1];

            var A = Convert.ToInt32(!p2 && p3) + Convert.ToInt32(!p3 && p4) +
                    Convert.ToInt32(!p4 && p5) + Convert.ToInt32(!p5 && p6) +
                    Convert.ToInt32(!p6 && p7) + Convert.ToInt32(!p7 && p8) +
                    Convert.ToInt32(!p8 && p9) + Convert.ToInt32(!p9 && p2);
            return A;
        }

        private static int NumberOfNonZeroNeighbors(int x, int y, bool[][] s)
        {
            var count = 0;
            if (s[x - 1][y]) count++;
            if (s[x - 1][y + 1]) count++;
            if (s[x - 1][y - 1]) count++;
            if (s[x][y + 1]) count++;
            if (s[x][y - 1]) count++;
            if (s[x + 1][y]) count++;
            if (s[x + 1][y + 1]) count++;
            if (s[x + 1][y - 1]) count++;
            return count;
        }
    }
}
