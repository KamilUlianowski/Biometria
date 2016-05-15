using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace Biometria_Projekt
{
   static class DebugFolder
    {
        public static string GetApplicationPath()
        {
           string path = System.IO.Path.GetDirectoryName(
              System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            return path.Remove(0, 6) + @"\Help.jpg";
        }

      
    }
}
