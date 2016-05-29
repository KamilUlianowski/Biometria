using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Imaging;
using Biometria_Projekt.Classes;
using Biometria_Projekt.Windows;
using CsvHelper;
using Ionic.Zip;
using Microsoft.Win32;
using Point = System.Windows.Point;
using Region = Biometria_Projekt.Classes.Region;

namespace Biometria_Projekt
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private Filters _filter;
        private WriteableBitmap _bitmap;
        private BitmapImage _image;
        private ImageProperties _imagaProperties;
        private ImageOperations _imageOperations;
        private byte[] _pixels, _changedPixels;
        private string _source;
        private int _width;
        private int _height;
        private int _stride;
        private int _arraySize;
        private HistogramWindow histogramWindow;
        private PixelPropertiesWindow pixelPropertiesWindow;
        private List<Vector1> listOfVectors1;
        private List<Vector2> listOfVectors2;

        public MainWindow()
        {
            InitializeComponent();
            _imagaProperties = new ImageProperties();
            DataContext = this;
        }

        public string Source
        {
            get { return _source; }
            set
            {
                _source = value;
                OnPropertyChanged("Source");
            }
        }

        public WriteableBitmap Bitmap
        {
            get { return _bitmap; }
            set
            {
                _bitmap = value;
                OnPropertyChanged("Bitmap");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
            _imagaProperties.Width = SourceData.PixelWidth;
            _imagaProperties.Height = SourceData.PixelHeight;
            _imagaProperties.Stride = (_bitmap.PixelWidth * _bitmap.Format.BitsPerPixel + 7) / 8;
            _imagaProperties.ArraySize = _stride * _height;
            _imagaProperties.Pixels = new byte[_arraySize];
            _imagaProperties.ChangedPixels = new byte[_arraySize];
            _imagaProperties.Bitmap = new WriteableBitmap(_image);

            _imageOperations = new ImageOperations { imgProperties = _imagaProperties };
            _filter = new Filters();
        }

        public void DrawChangePicture()
        {
            var rect = new Int32Rect(0, 0, _width, _height);
            Bitmap.WritePixels(rect, _changedPixels, _stride, 0);
        }

        public void DrawChangePicture(byte[] pixels)
        {
            var rect = new Int32Rect(0, 0, _width, _height);
            Bitmap.WritePixels(rect, pixels, _stride, 0);
        }

        public void ChangePixel(int x, int y, int r, int g, int b)
        {
            try
            {
                var index = ImageOperations.GetIndexOfPixel(x, y, _stride);
                _changedPixels[index + 2] = (byte)r;
                _changedPixels[index + 1] = (byte)g;
                _changedPixels[index] = (byte)b;
            }
            catch
            {
                return;
            }
        }

        public void ChangePictureWithLUT(int[] LUT)
        {
            for (var i = 0; i < _width; i++)
            {
                for (var j = 0; j < _height; j++)
                {
                    var index = ImageOperations.GetIndexOfPixel(i, j, _stride);

                    ChangePixel(i, j, _changedPixels[index + 2], _changedPixels[index + 1], _changedPixels[index]);

                    _changedPixels[index + 2] = (byte)LUT[_changedPixels[index + 2]];
                    _changedPixels[index + 1] = (byte)LUT[_changedPixels[index + 1]];
                    _changedPixels[index] = (byte)LUT[_changedPixels[index]];
                }
            }
        }

        public void ChangePictureWithLUT(int[] R, int[] G, int[] B)
        {
            for (var i = 0; i < _width; i++)
            {
                for (var j = 0; j < _height; j++)
                {
                    var index = ImageOperations.GetIndexOfPixel(i, j, _stride);

                    ChangePixel(i, j, _changedPixels[index + 2], _changedPixels[index + 1], _changedPixels[index]);

                    _changedPixels[index + 2] = (byte)R[_changedPixels[index + 2]];
                    _changedPixels[index + 1] = (byte)G[_changedPixels[index + 1]];
                    _changedPixels[index] = (byte)B[_changedPixels[index]];
                }
            }
        }

        private void StrechingClick(object sender, RoutedEventArgs e)
        {
            var a = int.Parse(TextBoxA.Text);
            var b = int.Parse(TextBoxB.Text);

            var LUT = ImageOperations.GetLUTForHistogramStreching(_changedPixels, a, b);

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

            var pixel = pixelPropertiesWindow.pixel;
            if (pixel == null) return;
            ChangePixel(pixel.x, pixel.y, pixel.r, pixel.g, pixel.b);
            DrawChangePicture();
        }


        private void Brightness_Click(object sender, RoutedEventArgs e)
        {
            var LUT = ImageOperations.GetLUTForChangeTheBrightness(double.Parse(TextBoxBrighter.Text));
            if (_pixels != null)
            {
                Array.Copy(_pixels, _changedPixels, _pixels.Length);

                ChangePictureWithLUT(LUT);

                DrawChangePicture();
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
            var LUT = Binaryzation.GetLUTForBinaryzation(int.Parse(TextBoxBinaryzation.Text));

            ChangePictureWithLUT(LUT);

            DrawChangePicture();
        }

        private void BinaryzationWithNiblack_Click(object sender, RoutedEventArgs e)
        {

            MakeGrayImage();
            ChangePictureNiblack();
            DrawChangePicture();
        }

        private void SaveFile(object sender, RoutedEventArgs e)
        {
            BitmapEncoder encoder;
            var source = DebugFolder.GetSavePath();
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
            using (var stream = new FileStream(source, FileMode.Create))
                encoder.Save(stream);
        }

        private void MakeGrayImage(object sender, RoutedEventArgs e)
        {
            MakeGrayImage();
        }

        private void MakeGrayImage()
        {
            for (var i = 0; i < _width; i++)
            {
                for (var j = 0; j < _height; j++)
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
            var treshold = Binaryzation.GetOtsuTreshold(histogramWindow.GetHistogramAverageVaues(),
                _changedPixels.Length / 4);

            var LUT = Binaryzation.GetLUTForBinaryzation(treshold);

            ChangePictureWithLUT(LUT);

            DrawChangePicture();
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            Source = DebugFolder.GetPath();
            if (Source == null) return;

            _source = Validation.Check8bppFormat(_source);

            _image = new BitmapImage(new Uri(_source));
            _imagaProperties.Image = new BitmapImage(new Uri(_source));
            GetDimnesions();
            _bitmap.CopyPixels(_pixels, _stride, 0);
            _bitmap.CopyPixels(_changedPixels, _stride, 0);
            _imagaProperties.Bitmap.CopyPixels(_imagaProperties.Pixels, _imagaProperties.Stride, 0);
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
            for (var i = 0; i < _width; i++)
            {
                for (var j = 0; j < _height; j++)
                {
                    var index = ImageOperations.GetIndexOfPixel(i, j, _stride);
                    var treshold = CalculateTreshold(i, j, 15, 15);

                    if (_changedPixels[index] < treshold)
                    {
                        _changedPixels[index] = 0;
                        _changedPixels[index + 1] = 0;
                        _changedPixels[index + 2] = 0;
                    }
                    else
                    {
                        _changedPixels[index] = 255;
                        _changedPixels[index + 1] = 255;
                        _changedPixels[index + 2] = 255;
                    }
                }
            }
        }

        public int CalculateTreshold(int x, int y, int sideA, int sideB)
        {
            var avg = GetAverageColor(x, y, sideA, sideB);
            var odch = StandardDeviation(x, y, sideA, sideB, avg);
            var prog = avg + -0.7 * odch;
            return (int)prog;
        }

        public double GetAverageColor(int x, int y, int sideA, int sideB)
        {
            // nums.Clear();
            double sum = 0;
            //  int counter = 0;
            sideA = sideA / 2;
            sideB = sideB / 2;
            var startX = x - sideA >= 0 ? x - sideA : 0;
            var startY = y - sideB >= 0 ? y - sideB : 0;
            var endX = x + sideA <= _width ? x + sideA : _width;
            var endY = y + sideB <= _height ? y + sideB : _height;
            var counter = (endY - startY) * (endX - startX);
            for (var i = startX; i < endX; i++)
            {
                //if (i < 0 || i >= _width) continue;
                for (var j = startY; j < endY; j++)
                {
                    //     if (j < 0 || j >= _height) continue;
                    var index = ImageOperations.GetIndexOfPixel(i, j, _stride);
                    sum += _pixels[index];
                    //   counter++;
                }
            }
            return sum / counter;
        }

        public double StandardDeviation(int x, int y, int sideA, int sideB, double avg)
        {
            double variance = 0;
            //double counter = 0;
            sideA = sideA / 2;
            sideB = sideB / 2;
            var startX = x - sideA >= 0 ? x - sideA : 0;
            var startY = y - sideB >= 0 ? y - sideB : 0;
            var endX = x + sideA <= _width ? x + sideA : _width;
            var endY = y + sideB <= _height ? y + sideB : _height;
            var counter = (endY - startY) * (endX - startX);

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

        public void LinearFiltr()
        {
            var maskWindow = new FilterMaskWindow();
            maskWindow.ShowDialog();
            var mask = maskWindow.MaskTable;
            var maskSum = 0;
            foreach (var obj in mask)
            {
                maskSum += obj;
            }
            for (var i = 1; i < _width - 1; i++)
            {
                for (var j = 1; j < _height - 1; j++)
                {
                    var green = 0;
                    var blue = 0;
                    var red = green = blue = 0;
                    for (var x = i - 1; x <= i + 1; x++)
                    {
                        for (var y = j - 1; y <= j + 1; y++)
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
                    if (maskSum != 0)
                    {
                        blue /= maskSum;
                        red /= maskSum;
                        green /= maskSum;
                    }
                    blue = Math.Max(0, Math.Min(255, blue));
                    red = Math.Max(0, Math.Min(255, red));
                    green = Math.Max(0, Math.Min(255, green));


                    ChangePixel(i, j, red, green, blue);
                }
            }
        }


        public void KuwaharFilter()
        {
            for (var x = 2; x < _width - 2; x++)
            {
                for (var y = 2; y < _height - 2; y++)
                {
                    var regions = new List<Region>
                    {
                        new Region(x - 1, y - 1),
                        new Region(x + 1, y - 1),
                        new Region(x - 1, y + 1),
                        new Region(x + 1, y + 1)
                    };
                    foreach (var region in regions)
                    {
                        region.AvgBrightnessRed = GetAvgBrithtnessKuwahar(region.StartX, region.StartY, Colors.Red);
                        region.AvgBrightnessGreen = GetAvgBrithtnessKuwahar(region.StartX, region.StartY, Colors.Green);
                        region.AvgBrightnessBlue = GetAvgBrithtnessKuwahar(region.StartX, region.StartY, Colors.Blue);
                        region.VarianceRed = GetVarianceKuwahar(region.StartX, region.StartY, region.AvgBrightnessRed,
                            Colors.Red);
                        region.VarianceGreen = GetVarianceKuwahar(region.StartX, region.StartY,
                            region.AvgBrightnessGreen, Colors.Green);
                        region.VarianceBlue = GetVarianceKuwahar(region.StartX, region.StartY, region.AvgBrightnessBlue,
                            Colors.Blue);
                    }
                    var minRegionRed = regions.OrderBy(par => par.VarianceRed).FirstOrDefault();
                    var minRegionGreen = regions.OrderBy(par => par.VarianceGreen).FirstOrDefault();
                    var minRegionBlue = regions.OrderBy(par => par.VarianceBlue).FirstOrDefault();
                    ChangePixel(x, y, minRegionRed.AvgBrightnessRed, minRegionGreen.AvgBrightnessGreen,
                        minRegionBlue.AvgBrightnessBlue);
                }
            }
            DrawChangePicture();
        }

        public int GetAvgBrithtnessKuwahar(int startX, int startY, Colors color)
        {
            var avgBrightness = 0;
            for (var i = startX - 1; i <= startX + 1; i++)
            {
                for (var j = startY - 1; j <= startY + 1; j++)
                {
                    var index = ImageOperations.GetIndexOfPixel(i, j, _stride) + (int)color;
                    avgBrightness += _pixels[index];
                }
            }
            return avgBrightness / 9;
        }

        public double GetVarianceKuwahar(int startX, int startY, int avgBrightness, Colors color)
        {
            double variance = 0;
            for (var i = startX - 1; i <= startX + 1; i++)
            {
                for (var j = startY - 1; j <= startY + 1; j++)
                {
                    var index = ImageOperations.GetIndexOfPixel(i, j, _stride) + (int)color;
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
            for (var x = mask / 2; x < _width - mask / 2; x++)
            {
                for (var y = mask / 2; y < _height - mask / 2; y++)
                {
                    var middleR = GetMiddlePixel(x, y, mask, Colors.Red);
                    var middleG = GetMiddlePixel(x, y, mask, Colors.Green);
                    var middleB = GetMiddlePixel(x, y, mask, Colors.Blue);
                    ChangePixel(x, y, middleR, middleG, middleB);
                }
                ;
            }
            DrawChangePicture();
        }

        public int GetMiddlePixel(int startX, int startY, int mask, Colors color)
        {
            var maskPixels = new List<int>();
            for (var i = startX - mask / 2; i <= startX + mask / 2; i++)
            {
                for (var j = startY - mask / 2; j <= startY + mask / 2; j++)
                {
                    var index = ImageOperations.GetIndexOfPixel(i, j, _stride);
                    maskPixels.Add(_pixels[index + (int)color]);
                }
            }
            return maskPixels.OrderBy(x => x).Skip(maskPixels.Count / 2).First();
        }

        private void MedianaFiltrClick(object sender, RoutedEventArgs e)
        {
            var maskWindow = new MaskWindow();
            maskWindow.ShowDialog();
            var mask = maskWindow.MaskSize;
            MedianFilter(mask);
        }

        private void Thinning_Click(object sender, RoutedEventArgs e)
        {
            var t = _imageOperations.Image2Bool();
            t = Thinning.ZhangSuenThinning(t);
            _imagaProperties.ChangedPixels = _imageOperations.Bool2Image(t);
            _changedPixels = _imagaProperties.ChangedPixels;
            DrawChangePicture(_imagaProperties.ChangedPixels);

        }

        private void Branching_Click(object sender, RoutedEventArgs e)
        {
            var LUT = Binaryzation.GetLUTForBinaryzation(100);
            ChangePictureWithLUT(LUT);
            _imagaProperties.ChangedPixels = _changedPixels;
            var points = Minutiae.FindBranching(_imagaProperties);
            MinutiaeDecoration(points, 15);

        }

        private void MinutiaeDecoration(List<Point> points, int sizeOfSquare)
        {
            foreach (var point in points)
            {

                for (int x = (int)point.X - sizeOfSquare / 2; x <= (int)point.X + sizeOfSquare / 2; x++)
                {
                    ChangePixel(x, (int)point.Y - sizeOfSquare / 2, 0, 0, 255);
                }
                for (int x = ((int)point.X - sizeOfSquare / 2) + 1; x <= ((int)point.X + sizeOfSquare / 2) - 1; x++)
                {
                    ChangePixel(x, (int)point.Y + sizeOfSquare / 2, 0, 0, 255);
                }
                for (int y = ((int)point.Y - sizeOfSquare / 2) + 1; y <= (int)point.Y + sizeOfSquare / 2; y++)
                {
                    ChangePixel((int)point.X - sizeOfSquare / 2, y, 0, 0, 255);
                }
                for (int y = ((int)point.Y - sizeOfSquare / 2) + 1; y <= (int)point.Y + sizeOfSquare / 2; y++)
                {
                    ChangePixel((int)point.X + sizeOfSquare / 2, y, 0, 0, 255);
                }
            }
            DrawChangePicture();
        }

        private void RidgeEnd_Click(object sender, RoutedEventArgs e)
        {
            var LUT = Binaryzation.GetLUTForBinaryzation(100);
            ChangePictureWithLUT(LUT);
            _imagaProperties.ChangedPixels = _changedPixels;
            var points = Minutiae.FindRidgeEndings(_imagaProperties, false);
            MinutiaeDecoration(points, 15);
        }

        private void OpenZip(object sender, RoutedEventArgs e)
        {
            using (ZipFile zip = ZipFile.Read(DebugFolder.GetPath()))
            {
                zip.ExtractAll(DebugFolder.GetSavePathToZip());
            }
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            var LUT = Binaryzation.GetLUTForBinaryzation(100);
            ChangePictureWithLUT(LUT);
            _imagaProperties.ChangedPixels = _changedPixels;
            var points = Minutiae.FindRidgeEndings(_imagaProperties, true);
            MinutiaeDecoration(points, 15);
        }

        private List<Table2> GetDataFromDatabase()
        {
            DataClasses1DataContext data1 = new DataClasses1DataContext();
            var q =
       from a in data1.GetTable<Table2>()
       select a;
            return q.ToList();
        }

        public List<Vector1> GetListOfVector1()
        {
            var vectors1 = new List<Vector1>();
            var list = GetDataFromDatabase();
            foreach (var row in list)
            {
                var clicks = Keystroke.ChangeStringToClicksStatistics(row.input1);
                var vector1 = Keystroke.CreateVector1(clicks, Int32.Parse(row.user_id.ToString()));
                if (vectors1.Exists(x => x.UserId == vector1.UserId))
                {
                    var item = vectors1.Single(x => x.UserId == vector1.UserId);
                    vectors1[vectors1.IndexOf(item)].SumTimes(vector1);
                }
                else
                {
                    vectors1.Add(vector1);
                }
            }
            return vectors1;
        }

        public List<Vector2> GetListOfVectors2()
        {
            var vectors2 = new List<Vector2>();
            var list = GetDataFromDatabase();
            foreach (var row in list)
            {
                var clicks = Keystroke.ChangeStringToClicksStatistics(row.input1);
                var vector2 = Keystroke.CreateVector2(clicks, Int32.Parse(row.user_id.ToString()));
                if (vectors2.Exists(x => x.UserId == vector2.UserId))
                {
                    var item = vectors2.Single(x => x.UserId == vector2.UserId);
                    vectors2[vectors2.IndexOf(item)].Sum(vector2);
                }
                else
                {
                    vectors2.Add(vector2);
                }
            }
            return vectors2;
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            DebugFolder.SaveToCsv(GetListOfVector1());
            MessageBox.Show(GetListOfVector1().Count.ToString());
        }

        private void MenuItemVector2_OnClick(object sender, RoutedEventArgs e)
        {
            DebugFolder.SaveToCsv(GetListOfVectors2());
            MessageBox.Show(GetListOfVectors2().Count.ToString());
        }


        private void MenuItemIdentyfication_OnClick(object sender, RoutedEventArgs e)
        {
            var listOfDistance = new Dictionary<int, double>();
            var identityWindow = new IdentificationWindow();
            identityWindow.ShowDialog();
            var clicks = Keystroke.ChangeStringToClicksStatistics(identityWindow.Text);
            var myVector1 = Keystroke.CreateVector1(clicks, 1000);
            if (listOfVectors1 == null)
            {
                listOfVectors1 = GetListOfVector1();
            }
            foreach (var item in listOfVectors1)
            {
                listOfDistance.Add(item.UserId, Keystroke.CountDistancesVector1(item, myVector1));
            }
            var closestDisancesVector1 = listOfDistance.OrderBy(x => x.Value).Take(3).ToList();
            MessageBox.Show(closestDisancesVector1[0].ToString() + " " +
                            closestDisancesVector1[1].ToString() + " " +
                            closestDisancesVector1[2].ToString());
        }

        private void MenuItemIdentyficationVektor2_OnClick(object sender, RoutedEventArgs e)
        {
            var listOfDistance = new Dictionary<int, double>();
            var identityWindow = new IdentificationWindow();
            identityWindow.ShowDialog();
            var clicks = Keystroke.ChangeStringToClicksStatistics(identityWindow.Text);
            var myVector2 = Keystroke.CreateVector2(clicks, 1000);
            if (listOfVectors2 == null)
            {
                listOfVectors2 = GetListOfVectors2();
            }

            foreach (var item in listOfVectors2)

            {
                listOfDistance.Add(item.UserId, Keystroke.CountDistancesVector2(item, myVector2));
            }
            var closestDisancesVector1 = listOfDistance.OrderBy(x => x.Value).Take(3).ToList();
            MessageBox.Show(closestDisancesVector1[0].ToString() + " " +
                            closestDisancesVector1[1].ToString() + " " +
                            closestDisancesVector1[2].ToString());
        }

        private void MenuItemIdentityUser_OnClick(object sender, RoutedEventArgs e)
        {
            var identityUser = new IdentityUserWindow();
            identityUser.ShowDialog();
            var texts = identityUser.Texts;
            foreach (var x in texts)
            {
                MessageBox.Show(x);
            }
        }
    }
}
