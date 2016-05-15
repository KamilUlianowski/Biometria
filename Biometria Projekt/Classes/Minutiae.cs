using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Biometria_Projekt.Classes
{
    class Minutiae
    {
        public static bool CheckCircuit(int x, int y, List<Point> points)
        {
            foreach (var point in points)
            {
                if (x >= point.X - 1 && x <= point.X + 1 && y >= point.Y - 1 && y <= point.Y + 1)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool FilterPoints(int x, int y, List<Point> points)
        {
            foreach (var point in points)
            {
                if (x >= point.X - 8 && x <= point.X + 8 && y >= point.Y - 8 && y <= point.Y + 8)
                {
                    return true;
                }
            }

            return false;
        }

        public static int CountIntersection(ImageProperties imgProperties, int sizeOfSquare, int x, int y)
        {

            int count = 0;
            List<Point> pointers = new List<Point>();
            for (int a = x - sizeOfSquare / 2; a <= x + sizeOfSquare / 2; a++)
            {
                //  pointers.Add(new Point(a, y - sizeOfSquare / 2));

                if (ImageOperations.GetPixelValue(a, y - sizeOfSquare / 2, imgProperties) == 0)
                {
                    if (CheckCircuit(a, y - sizeOfSquare / 2, pointers) == false)
                    {
                        pointers.Add(new Point(a, y - sizeOfSquare / 2));
                        count++;
                    }
                    break;

                }
            }

            for (int a = (x - sizeOfSquare / 2) + 1; a <= (x + sizeOfSquare / 2) - 1; a++)
            {
                // pointers.Add(new Point(a, y + sizeOfSquare / 2));

                if (ImageOperations.GetPixelValue(a, y + sizeOfSquare / 2, imgProperties) == 0)
                {
                    if (CheckCircuit(a, y + sizeOfSquare / 2, pointers) == false)
                    {
                        pointers.Add(new Point(a, y + sizeOfSquare / 2));
                        count++;
                    }
                    break;

                }
            }

            for (int b = (y - sizeOfSquare / 2) + 1; b <= (y + sizeOfSquare / 2); b++)
            {
                //  pointers.Add(new Point(x - sizeOfSquare / 2, b));

                if (ImageOperations.GetPixelValue(x - sizeOfSquare / 2, b, imgProperties) == 0)
                {
                    if (CheckCircuit(x - sizeOfSquare / 2, b, pointers) == false)
                    {
                        pointers.Add(new Point(x - sizeOfSquare / 2, b));
                        count++;
                    }
                    break;

                }
            }

            for (int b = (y - sizeOfSquare / 2) + 1; b <= y + sizeOfSquare / 2; b++)
            {
                // pointers.Add(new Point(x + sizeOfSquare / 2, b));

                if (ImageOperations.GetPixelValue(x + sizeOfSquare / 2, b, imgProperties) == 0)
                {
                    if (CheckCircuit(x + sizeOfSquare / 2, b, pointers) == false)
                    {
                        pointers.Add(new Point(x + sizeOfSquare / 2, b));
                        count++;
                    }
                    break;

                }
            }
            return count;
        }

        public static List<Point> FindBranching(ImageProperties imgProperties)
        {
            var points = new List<Point>();
            for (int x = 5; x < imgProperties.Width - 5; x++)
            {
                for (int y = 5; y < imgProperties.Height - 5; y++)
                {
                    if (ImageOperations.GetPixelValue(x, y, imgProperties) == 0)
                    {
                        int countSquare5 = CountIntersection(imgProperties, 5, x, y);
                        int countSquare9 = CountIntersection(imgProperties, 9, x, y);
                        if (countSquare9 == 3 && countSquare5 == 3)
                        {
                            if (FilterPoints(x, y, points) == false)
                            {
                                points.Add(new Point(x, y));

                            }

                        }
                    }
                }

            }
            return points;
        }

        public static bool CheckNeighbours(int x, int y, ImageProperties imgProperties)
        {
            var count = 0;
            for (int a = x - 1; a <= x + 1; a++)
            {
                for (int b = y - 1; b <= y + 1; b++)
                {
                    if (a == x && b == y)
                    {
                    }
                    else
                    {
                        
                        if (ImageOperations.GetPixelValue(a, b, imgProperties) == 0)
                        {
                            count++;
                        }
                    }
                }
            }
            if (count == 1) return false;
            return true;
        }

        public static List<Point> FindRidgeEndings(ImageProperties imgProperties)
        {
            var points = new List<Point>();
            for (int x = 5; x < imgProperties.Width - 5; x++)
            {
                for (int y = 5; y < imgProperties.Height - 5; y++)
                {
                    if (ImageOperations.GetPixelValue(x, y, imgProperties) == 0)
                    {
                        if (CheckNeighbours(x, y, imgProperties) == false)
                        {
                            points.Add(new Point(x,y));
                        }

                    }
                }

            }
            points = FilterPoints(imgProperties, points);
            return points;
        }

        public static List<Point> FilterPoints(ImageProperties imgProperties, List<Point> points )
        {
            List<Point> filterPoints = new List<Point>();
            List<Point> tempPoints = new List<Point>();
            double minYLeft = 0;
            double maxYRight = 0;
            for (int x = 0; x < imgProperties.Width; x += 30)
            {
                foreach (var point in points)
                {
                    if (point.X > x && point.X < x + 30)
                    {
                        tempPoints.Add(point);
                    }
                }
                if (tempPoints.Count > 0)
                {
                    minYLeft = tempPoints.Min(help => help.Y);
                    maxYRight = tempPoints.Max(help => help.Y);
                    filterPoints.AddRange(tempPoints.Where(help => help.Y > minYLeft + 20 && help.Y < maxYRight - 20));
                    tempPoints.Clear();
                }
            }
            return filterPoints;
        } 

    }
}
