using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Biometria_Projekt.Classes
{
    internal class Minutiae
    {
        public static bool CheckCircuit(int x, int y, List<Point> points)
        {
            return points.Any(point => x >= point.X - 1 && x <= point.X + 1 && y >= point.Y - 1 && y <= point.Y + 1);
        }

        public static bool FilterPoints(int x, int y, List<Point> points)
        {
            return points.Any(point => x >= point.X - 8 && x <= point.X + 8 && y >= point.Y - 8 && y <= point.Y + 8);
        }

        public static int CountIntersection(ImageProperties imgProperties, int sizeOfSquare, int x, int y)
        {
            var count = 0;
            var pointers = new List<Point>();
            for (var a = x - sizeOfSquare / 2; a <= x + sizeOfSquare / 2; a++)
            {
                if (ImageOperations.GetPixelValue(a, y - sizeOfSquare / 2, imgProperties) != 0) continue;
                if (CheckCircuit(a, y - sizeOfSquare / 2, pointers) == false)
                {
                    pointers.Add(new Point(a, y - sizeOfSquare / 2));
                    count++;
                }
                break;
            }

            for (var a = x - sizeOfSquare / 2 + 1; a <= x + sizeOfSquare / 2 - 1; a++)
            {
                if (ImageOperations.GetPixelValue(a, y + sizeOfSquare / 2, imgProperties) != 0) continue;
                if (CheckCircuit(a, y + sizeOfSquare / 2, pointers) == false)
                {
                    pointers.Add(new Point(a, y + sizeOfSquare / 2));
                    count++;
                }
                break;
            }

            for (var b = y - sizeOfSquare / 2 + 1; b <= y + sizeOfSquare / 2; b++)
            {
                if (ImageOperations.GetPixelValue(x - sizeOfSquare / 2, b, imgProperties) != 0) continue;
                if (CheckCircuit(x - sizeOfSquare / 2, b, pointers) == false)
                {
                    pointers.Add(new Point(x - sizeOfSquare / 2, b));
                    count++;
                }
                break;
            }

            for (var b = y - sizeOfSquare / 2 + 1; b <= y + sizeOfSquare / 2; b++)
            {
                if (ImageOperations.GetPixelValue(x + sizeOfSquare / 2, b, imgProperties) != 0) continue;
                if (CheckCircuit(x + sizeOfSquare / 2, b, pointers) == false)
                {
                    pointers.Add(new Point(x + sizeOfSquare / 2, b));
                    count++;
                }
                break;
            }
            return count;
        }

        public static List<Point> FindBranching(ImageProperties imgProperties)
        {
            var points = new List<Point>();
            for (var x = 5; x < imgProperties.Width - 5; x++)
            {
                for (var y = 5; y < imgProperties.Height - 5; y++)
                {
                    if (ImageOperations.GetPixelValue(x, y, imgProperties) != 0) continue;
                    var countSquare5 = CountIntersection(imgProperties, 5, x, y);
                    var countSquare9 = CountIntersection(imgProperties, 9, x, y);
                    if (countSquare9 != 3 || countSquare5 != 3) continue;
                    if (FilterPoints(x, y, points) == false)
                    {
                        points.Add(new Point(x, y));
                    }
                }
            }
            return points;
        }

        public static bool CheckNeighbours(int x, int y, ImageProperties imgProperties)
        {
            var count = 0;
            for (var a = x - 1; a <= x + 1; a++)
            {
                for (var b = y - 1; b <= y + 1; b++)
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

        public static List<Point> FindRidgeEndings(ImageProperties imgProperties, bool filtrer)
        {
            var points = new List<Point>();
            for (var x = 5; x < imgProperties.Width - 5; x++)
            {
                for (var y = 5; y < imgProperties.Height - 5; y++)
                {
                    if (ImageOperations.GetPixelValue(x, y, imgProperties) != 0) continue;
                    if (CheckNeighbours(x, y, imgProperties) != false) continue;
                    if (filtrer == true)
                    {
                        if (CheckVerticalBorders(x, y, imgProperties) && CheckHorizontalBorders(x, y, imgProperties))
                        {
                            if (FilterPoints(x, y, points) == false)
                                points.Add(new Point(x, y));
                        }
                    }
                    else
                    {
                        if (FilterPoints(x, y, points) == false)
                            points.Add(new Point(x, y));
                    }
                }
            }
            return points;
        }

        public static bool CheckHorizontalBorders(int x, int y, ImageProperties imgProperties)
        {
            int count = 0;



            if (y < imgProperties.Height / 2)
            {
                for (var b = y - 25; b < y; b++)
                {
                    if (b < 0) continue;
                    if (ImageOperations.GetPixelValue(x, b, imgProperties) == 0)
                    {
                        count++;
                    }
                }
            }
            if (y > imgProperties.Height / 2)
            {
                for (var b = y; b < y + 25; b++)
                {
                    if (b >= imgProperties.Height) continue;
                    if (ImageOperations.GetPixelValue(x, b, imgProperties) == 0)
                    {
                        count++;
                    }
                }
            }
            if (count > 0) return true;
            return false;
        }

        public static bool CheckVerticalBorders(int x, int y, ImageProperties imgProperties)
        {
            var count = 0;
            if (x < imgProperties.Width / 2)
            {
                for (var a = x - 25; a < x; a++)
                {
                    if (a < 0) continue;
                    if (ImageOperations.GetPixelValue(a, y, imgProperties) == 0)
                    {
                        count++;
                    }
                }
            }
            if (x > imgProperties.Width / 2)
            {
                for (var a = x + 1; a <= x + 25; a++)
                {
                    if (a > imgProperties.Width) continue;
                    if (ImageOperations.GetPixelValue(a, y, imgProperties) == 0)
                    {
                        count++;
                    }
                }
            }
           
            if (count > 0)
            {
                return true;
            }
            return false;
        }

    }
}