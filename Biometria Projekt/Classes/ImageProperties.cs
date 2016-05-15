using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Biometria_Projekt.Classes
{
    public class ImageProperties
    {
        public byte[] Pixels { get; set; }
        public byte[] ChangedPixels { get; set; }
        public string Source { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Stride { get; set; }
        public int ArraySize { get; set; }
        public WriteableBitmap Bitmap { get; set; }
        public BitmapImage Image { get; set; }
    }
}
