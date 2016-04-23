using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Win32;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Biometria_Projekt.Windows;
using System.Drawing.Imaging;
using Biometria_Projekt.Classes;

namespace Biometria_Projekt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        int _width, _height, _stride, _arraySize;
        private string _source;
        private BitmapImage _image;
        private WriteableBitmap _bitmap;
        private byte[] _pixels, _changedPixels;
        HistogramWindow histogramWindow;
        PixelPropertiesWindow pixelPropertiesWindow;
        List<double> nums = new List<double>();

        public string Source
        {
            get { return _source; }
            set
            {
                this._source = value;
                this.OnPropertyChanged("Source");
            }
        }

        public WriteableBitmap Bitmap
        {
            get { return _bitmap; }
            set
            {
                this._bitmap = value;
                this.OnPropertyChanged("Bitmap");
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void GetDimnesions()
        {
            var SourceData = (BitmapSource)ImageFirst.Source;
            _width = SourceData.PixelWidth;
            _height = SourceData.PixelHeight;
            Bitmap = new WriteableBitmap(_image);
            _stride = (_bitmap.PixelWidth * _bitmap.Format.BitsPerPixel + 7) / 8;
            _arraySize = _stride * _height;
            _pixels = new byte[_arraySize];
            _changedPixels = new byte[_arraySize];


        }

        public void DrawChangePicture()
        {
            var rect = new Int32Rect(0, 0, _width, _height);
            Bitmap.WritePixels(rect, _changedPixels, _stride, 0);
        }

        public void ChangePixel(int x, int y, int r, int g, int b)
        {
            var index = ImageOperations.GetIndexOfPixel(x, y, _stride);
            _changedPixels[index + 2] = (byte)r;
            _changedPixels[index + 1] = (byte)g;
            _changedPixels[index] = (byte)b;
        }

        public void ChangePictureWithLUT(int[] LUT)
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    var index = ImageOperations.GetIndexOfPixel(i, j, _stride);

                    ChangePixel(i, j, _changedPixels[index + 2], _changedPixels[index + 1], _changedPixels[index]);

                    _changedPixels[index + 2] = (byte)(LUT[(int)_changedPixels[index + 2]]);
                    _changedPixels[index + 1] = (byte)(LUT[(int)_changedPixels[index + 1]]);
                    _changedPixels[index] = (byte)(LUT[(int)_changedPixels[index]]);
                }
            }
        }

        public void ChangePictureWithLUT(int[] R, int[] G, int[] B)
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    var index = ImageOperations.GetIndexOfPixel(i, j, _stride);

                    ChangePixel(i, j, _changedPixels[index + 2], _changedPixels[index + 1], _changedPixels[index]);

                    _changedPixels[index + 2] = (byte)(R[(int)_changedPixels[index + 2]]);
                    _changedPixels[index + 1] = (byte)(G[(int)_changedPixels[index + 1]]);
                    _changedPixels[index] = (byte)(B[(int)_changedPixels[index]]);
                }
            }
        }

        private void StrechingClick(object sender, RoutedEventArgs e)
        {

            var a = Int32.Parse(TextBoxA.Text);
            var b = Int32.Parse(TextBoxB.Text);

            int[] LUT = ImageOperations.GetLUTForHistogramStreching(_changedPixels, a, b);

            ChangePictureWithLUT(LUT);

            DrawChangePicture();
        }

        private void LookCoordinateButonClick(object sender, RoutedEventArgs e)
        {
            var viewPixelWindow = new ViewPixelWindow(_pixels, _changedPixels, _stride);
            viewPixelWindow.Show();


        }

        private void HistogramButton_Click(object sender, RoutedEventArgs e)
        {
            var hw = new HistogramWindow(_changedPixels);
            hw.Show();
            //    new ChartHistogramWindow(hw.GetHistogramAverageVaues(), hw.GetHistogramRedValues(), hw.GetHistogramGreenValues(), hw.GetHistogramBlueValues()).Show();
        }

        private void Histogramequalization_Click(object sender, RoutedEventArgs e)
        {
            var R = histogramWindow.GetHistogramRedValues();
            var G = histogramWindow.GetHistogramGreenValues();
            var B = histogramWindow.GetHistogramBlueValues();

            var CR = ImageOperations.GetCumulativeHistogram(R);
            var CG = ImageOperations.GetCumulativeHistogram(G);
            var CB = ImageOperations.GetCumulativeHistogram(B);

            var LUTR = ImageOperations.GetLUTFromCumulativeHistogram(CR, _width, _height);
            var LUTG = ImageOperations.GetLUTFromCumulativeHistogram(CG, _width, _height);
            var LUTB = ImageOperations.GetLUTFromCumulativeHistogram(CB, _width, _height);

            ChangePictureWithLUT(LUTR, LUTG, LUTB);

            DrawChangePicture();
        }

        private void ChangePixelButtonClick(object sender, RoutedEventArgs e)
        {
            pixelPropertiesWindow = new PixelPropertiesWindow();
            pixelPropertiesWindow.ShowDialog();

            PixelProperties pixel = pixelPropertiesWindow.pixel;
            if (pixel == null) return;
            ChangePixel(pixel.x, pixel.y, pixel.r, pixel.g, pixel.b);
            DrawChangePicture();
        }

        public void LinearFiltr()
        {
            var red = 0;
            var green = 0;
            var blue = 0;
            var maskWindow = new FilterMaskWindow();
            maskWindow.ShowDialog();
            var mask = maskWindow.MaskTable;
            for (int i = 1; i < _width - 1; i++)
            {
                for (int j = 1; j < _height - 1; j++)
                {
                    red = green = blue = 0;
                    for (int x = i - 1; x <= i + 1; x++)
                    {
                        for (int y = j - 1; y <= j + 1; y++)
                        {
                            var x2 = 0;
                            var y2 = 0;
                            if (x == i) x2 = 1;
                            if (x < i) x2 = 0;
                            if (x > i) x2 = 2;
                            if (y == j) y2 = 1;
                            if (y < j) y2 = 0;
                            if (y > j) y2 = 2;
                            var index2 = ImageOperations.GetIndexOfPixel(x, y, _stride);
                            red += mask[x2, y2] * _pixels[index2 + 2];
                            green += mask[x2, y2] * _pixels[index2 + 1];
                            blue += mask[x2, y2] * _pixels[index2];
                        }
                    }

                    blue = Math.Max(0, Math.Min(255, blue));
                    red = Math.Max(0, Math.Min(255, blue));
                    green = Math.Max(0, Math.Min(255, blue));
                    var index = ImageOperations.GetIndexOfPixel(i, j, _stride);

                    ChangePixel(i, j, _changedPixels[index + 2], _changedPixels[index + 1], _changedPixels[index]);

                    _changedPixels[index + 2] = (byte)red;
                    _changedPixels[index + 1] = (byte)green;
                    _changedPixels[index] = (byte)blue;
                }
            }
        }


        private void Brightness_Click(object sender, RoutedEventArgs e)
        {
            int[] LUT = ImageOperations.GetLUTForChangeTheBrightness(double.Parse(TextBoxBrighter.Text));
            if (_pixels != null)
            {
                Array.Copy(_pixels, _changedPixels, _pixels.Length);

                ChangePictureWithLUT(LUT);

                DrawChangePicture();
            }

        }

        public void Check8bppFormat()
        {
            System.Drawing.Image image1 = System.Drawing.Image.FromFile(_source);
            var p = image1.PixelFormat;

            if (p == System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
            {
                Random rand = new Random();

                string path = DebugFolder.GetApplicationPath();
                path = path.Remove(path.Length - 4);
                path += +rand.Next(0, 1000) + @".Jpeg";
                image1.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                _source = path;
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            var z = new ZoomImageWindow(_source);
            z.Show();
        }

        private void ShowFields(object sender, RoutedEventArgs e)
        {
            StackPanelBrithness.Visibility = Visibility.Visible;
            StackPanelStreching.Visibility = Visibility.Visible;
            StackPanelBinaryzation.Visibility = Visibility.Hidden;
        }

        private void BinaryzationWithOwnThreshold_Click(object sender, RoutedEventArgs e)
        {
            MakeGrayImage();
            var LUT = Binaryzation.GetLUTForBinaryzation(Int32.Parse(TextBoxBinaryzation.Text));

            ChangePictureWithLUT(LUT);

            DrawChangePicture();
        }

        private void BinaryzationWithNiblack_Click(object sender, RoutedEventArgs e)
        {
            MakeGrayImage();
            ChangePictureNiblack();
            DrawChangePicture();
        }

        private string GetPath()
        {
            var browser = new OpenFileDialog();
            if (browser.ShowDialog() != true) return null;
            return browser.FileName;
        }

        private string GetSavePath()
        {
            var browser = new SaveFileDialog();
            browser.Filter = "Gif Image (.gif)|*.gif|JPEG Image (.jpeg)|*.jpeg|Tiff Image (.tiff)|*.tiff";
            if (browser.ShowDialog() != true) return null;
            return browser.FileName;
        }

        private void SaveFile(object sender, RoutedEventArgs e)
        {
            BitmapEncoder encoder;
            string source = GetSavePath();
            if (source.EndsWith("gif"))
            {
                encoder = new GifBitmapEncoder();
            }
            else if (source.EndsWith("jpeg") || source.EndsWith("jpg"))
            {
                encoder = new JpegBitmapEncoder();
            }
            else
            {
                encoder = new TiffBitmapEncoder();
            }

            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)ImageAfterChange.Source));
            using (FileStream stream = new FileStream(source, FileMode.Create))
                encoder.Save(stream);
        }

        private void MakeGrayImage(object sender, RoutedEventArgs e)
        {
            MakeGrayImage();
        }

        private void MakeGrayImage()
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {
                    var index = ImageOperations.GetIndexOfPixel(i, j, _stride);

                    var color = (_changedPixels[index] + _changedPixels[index + 1] + _changedPixels[index + 2]) / 3;
                    _changedPixels[index + 2] = (byte)color;
                    _changedPixels[index + 1] = (byte)color;
                    _changedPixels[index] = (byte)color;
                }
            }
            DrawChangePicture();
        }

        public void BinaryzationWithOtsu_Click(object sender, RoutedEventArgs e)
        {
            MakeGrayImage();
            int treshold = Binaryzation.GetOtsuTreshold(histogramWindow.GetHistogramAverageVaues(), _changedPixels.Length / 4);

            var LUT = Binaryzation.GetLUTForBinaryzation(treshold);

            ChangePictureWithLUT(LUT);

            DrawChangePicture();
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            Source = GetPath();
            if (Source == null) return;

            Check8bppFormat();

            _image = new BitmapImage(new Uri(_source));
            GetDimnesions();
            _bitmap.CopyPixels(_pixels, _stride, 0);
            _bitmap.CopyPixels(_changedPixels, _stride, 0);
            histogramWindow = new HistogramWindow(_changedPixels);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            _pixels.CopyTo(_changedPixels, 0);
            DrawChangePicture();
        }

        private void Exercise2_Click(object sender, RoutedEventArgs e)
        {
            StackPanelBrithness.Visibility = Visibility.Visible;
        }

        public void ChangePictureNiblack()
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _height; j++)
                {

                    var index = ImageOperations.GetIndexOfPixel(i, j, _stride);
                    var treshold = CalculateTreshold(i, j, int.Parse(TextBoxNiblackSizeOfWindowA.Text), int.Parse(TextBoxNiblackSizeOfWindowB.Text));
                    if (_changedPixels[index] < treshold)
                    {
                        _changedPixels[index] = (byte)0;
                        _changedPixels[index + 1] = (byte)0;
                        _changedPixels[index + 2] = (byte)0;
                    }
                    else
                    {
                        _changedPixels[index] = (byte)255;
                        _changedPixels[index + 1] = (byte)255;
                        _changedPixels[index + 2] = (byte)255;
                    }

                }
            }
        }

        public int CalculateTreshold(int x, int y, int sideA, int sideB)
        {
            var avg = GetAverageColor(x, y, sideA, sideB);
            var odch = StandardDeviation(x, y, sideA, sideB, avg);
            var prog = avg + (double.Parse(TextBoxNilbackTresholdParameter.Text) * odch);
            return (int)prog;

        }

        public double GetAverageColor(int x, int y, int sideA, int sideB)
        {
            // nums.Clear();
            double sum = 0;
            //  int counter = 0;
            sideA = sideA / 2;
            sideB = sideB / 2;
            int startX = ((x - sideA) >= 0) ? x - sideA : 0;
            int startY = ((y - sideB) >= 0) ? y - sideB : 0;
            int endX = ((x + sideA) <= _width) ? x + sideA : _width;
            int endY = ((y + sideB) <= _height) ? y + sideB : _height;
            int counter = (endY - startY) * (endX - startX);
            for (int i = startX; i < endX; i++)
            {
                //if (i < 0 || i >= _width) continue;
                for (int j = startY; j < endY; j++)
                {
                    //     if (j < 0 || j >= _height) continue;
                    var index = ImageOperations.GetIndexOfPixel(i, j, _stride);
                    sum += _pixels[index];
                    //   counter++;
                }
            }
            return (sum / counter);
        }

        public double StandardDeviation(int x, int y, int sideA, int sideB, double avg)
        {

            double variance = 0;
            //double counter = 0;
            sideA = sideA / 2;
            sideB = sideB / 2;
            int startX = ((x - sideA) >= 0) ? x - sideA : 0;
            int startY = ((y - sideB) >= 0) ? y - sideB : 0;
            int endX = ((x + sideA) <= _width) ? x + sideA : _width;
            int endY = ((y + sideB) <= _height) ? y + sideB : _height;
            int counter = (endY - startY) * (endX - startX);

            for (var i = startX; i < endX; i++)
            {
                for (var j = startY; j < endY; j++)
                {
                    var index = ImageOperations.GetIndexOfPixel(i, j, _stride);
                    variance += (_pixels[index] - avg) * (_pixels[index] - avg);
                }

            }
            variance = variance / counter;
            return Math.Sqrt(variance);
        }

        private void LinearFiltrClick(object sender, RoutedEventArgs e)
        {
            LinearFiltr();
            DrawChangePicture();
        }

        public void KuwaharFilter()
        {
            for (int x = 2; x < _width - 2; x++)
            {
                for (int y = 2; y < _height - 2; y++)
                {

                    List<Region> regions = new List<Region>()
                    {

                    new Region(GetAvgBrithtnessKuwahar(x-1,y-1), GetVarianceKuwahar(x - 1, y - 1, GetAvgBrithtnessKuwahar(x - 1, y - 1))),
                    new Region(GetAvgBrithtnessKuwahar(x + 1, y - 1), GetVarianceKuwahar(x + 1, y - 1, GetAvgBrithtnessKuwahar(x + 1, y - 1))),
                    new Region(GetAvgBrithtnessKuwahar(x - 1, y + 1), GetVarianceKuwahar(x - 1, y + 1, GetAvgBrithtnessKuwahar(x - 1, y + 1))),
                    new Region(GetAvgBrithtnessKuwahar(x + 1, y + 1), GetVarianceKuwahar(x + 1, y + 1, GetAvgBrithtnessKuwahar(x + 1, y + 1)))

                };

                    var minRegion = regions.OrderBy(par => par.Variance).FirstOrDefault();
                    ChangePixel(x, y, minRegion.AvgBrightness, minRegion.AvgBrightness, minRegion.AvgBrightness);
                }
            }
            DrawChangePicture();
        }

        public int GetAvgBrithtnessKuwahar(int startX, int startY)
        {
            int avgBrightness = 0;
            for (int i = startX - 1; i <= startX + 1; i++)
            {
                for (int j = startY - 1; j <= startY + 1; j++)
                {
                    int index = ImageOperations.GetIndexOfPixel(i, j, _stride);
                    avgBrightness += _pixels[index];
                }
            }
            return (int)avgBrightness / 9;
        }

        public double GetVarianceKuwahar(int startX, int startY, int avgBrightness)
        {
            double variance = 0;
            for (int i = startX - 1; i <= startX + 1; i++)
            {
                for (int j = startY - 1; j <= startY + 1; j++)
                {
                    int index = ImageOperations.GetIndexOfPixel(i, j, _stride);
                    variance += (avgBrightness - _pixels[index]) * (avgBrightness - _pixels[index]);
                }
            }
            return variance / 9;
        }

        private void KuwaharFiltrClick(object sender, RoutedEventArgs e)
        {
            KuwaharFilter();
        }

        public void MedianFilter(int mask)
        {
            for (int x = mask/2; x < _width - mask/2; x++)
            {
                for (int y = mask / 2; y < _height - mask / 2; y++)
                {
                    var middleR = GetMiddlePixel(x, y, mask,Colors.Red);
                    var middleG = GetMiddlePixel(x, y, mask,Colors.Green);
                    var middleB = GetMiddlePixel(x, y, mask,Colors.Blue);
                    ChangePixel(x,y,middleR,middleG,middleB);
                };              
            }
            DrawChangePicture();
        }

        public int GetMiddlePixel(int startX, int startY, int mask, Colors color)
        {
            var maskPixels = new List<int>();
            for (int i = startX - mask / 2; i <= startX + mask / 2; i++)
            {
                for (int j = startY - mask / 2; j <= startY + mask / 2; j++)
                {
                    int index = ImageOperations.GetIndexOfPixel(i, j, _stride);
                    maskPixels.Add(_pixels[index+(int)color]);                 
                }
            }
            return maskPixels.OrderBy(x => x).Skip(maskPixels.Count/2).First();
        }

        private void MedianaFiltrClick(object sender, RoutedEventArgs e)
        {           
            var maskWindow = new MaskWindow();
            maskWindow.ShowDialog();
            int mask = maskWindow.MaskSize;
            MedianFilter(mask);
        }
    }
}

