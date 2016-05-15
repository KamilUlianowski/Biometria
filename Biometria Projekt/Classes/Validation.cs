using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biometria_Projekt.Classes
{
    class Validation
    {
        public static string Check8bppFormat(string source)
        {
            var image1 = Image.FromFile(source);
            var p = image1.PixelFormat;

            if (p == PixelFormat.Format8bppIndexed)
            {
                var rand = new Random();

                var path = DebugFolder.GetApplicationPath();
                path = path.Remove(path.Length - 4);
                path += +rand.Next(0, 1000) + @".Jpeg";
                image1.Save(path, ImageFormat.Jpeg);
                return path;
            }
            return source;
        }
    }
}
