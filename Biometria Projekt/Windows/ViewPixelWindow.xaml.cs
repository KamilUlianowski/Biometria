using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Biometria_Projekt.Windows
{
    /// <summary>
    /// Interaction logic for ViewPixelWindow.xaml
    /// </summary>
    public partial class ViewPixelWindow : Window
    {
        private byte[] pixels, changePixels;
        private int stride;
        public ViewPixelWindow(byte[] pix, byte[] changePix, int _stride)
        {
            InitializeComponent();
            pixels = pix;
            changePixels = changePix;
            stride = _stride;
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            var index = ImageOperations.GetIndexOfPixel(Int32.Parse(CoordinateX.Text), Int32.Parse(CoordinateY.Text), stride);
            LabelX.Content = "X : " + CoordinateX.Text;
            LabelY.Content = "Y : " + CoordinateY.Text;
            LabelR.Content = "R : " + pixels[index + 2];
            LabelG.Content = "G : " + pixels[index + 1];
            LabelB.Content = "B : " + pixels[index];

            LabelChangeX.Content = "X : " + CoordinateX.Text;
            LabelChangeY.Content = "Y : " + CoordinateY.Text;
            LabelChangeR.Content = "R : " + changePixels[index + 2];
            LabelChangeG.Content = "G : " + changePixels[index + 1];
            LabelChangeB.Content = "B : " + changePixels[index];
        }
    }
}
