using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Ionic.Zip;
using System.IO.Compression;
using Biometria_Projekt.Classes;
using CsvHelper;
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

        public static string GetPath()
        {
            var browser = new OpenFileDialog();
            if (browser.ShowDialog() != true) return null;
            return browser.FileName;
        }

        public static string GetSavePath()
        {
            var browser = new SaveFileDialog();
            browser.Filter = "Gif Image (.gif)|*.gif|JPEG Image (.jpeg)|*.jpeg|Tiff Image (.tiff)|*.tiff";
            if (browser.ShowDialog() != true) return null;
            return browser.FileName;
        }

        public static string GetSavePathToZip()
        {
            var browser = new SaveFileDialog();
            if (browser.ShowDialog() != true) return null;
            return browser.FileName;
        }

        public static void SaveToCsv(List<Vector1> listOfUsers)
        {
            using (var sw = new StreamWriter(@"vector1.csv"))
            {
                var writer = new CsvWriter(sw);
                writer.WriteField("UserId");
                for (int i = 65; i <= 90; i++)
                {
                    string s = ((char) i).ToString();
                    writer.WriteField(s);
                }
                writer.NextRecord();
                foreach (var item in listOfUsers)
                {
                    writer.WriteField(item.UserId);
                    foreach (var key in item.Keys)
                    {
                        writer.WriteField(key.AverageTime);
                    }
                    writer.NextRecord();
                }
              
            }
        }

        public static void SaveToCsv(List<Vector2> listOfUsers)
        {
            using (var sw = new StreamWriter(@"vector2.csv"))
            {
                var writer = new CsvWriter(sw);
                writer.WriteRecords(listOfUsers);

            }
        }


    }
}
