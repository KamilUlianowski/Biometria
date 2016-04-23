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

namespace Biometria_Projekt
{
    /// <summary>
    /// Interaction logic for PixelPropertiesWindow.xaml
    /// </summary>
    public partial class PixelPropertiesWindow : Window
    {
        public PixelProperties pixel;

        public PixelPropertiesWindow()
        {
            InitializeComponent();
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            pixel = new PixelProperties
            {
                x = Int32.Parse(CoordinateX.Text),
                y = Int32.Parse(CoordinateY.Text),
                r = Int32.Parse(textBlockR.Text),
                g = Int32.Parse(textBlockG.Text),
                b = Int32.Parse(textBlockB.Text)
            };

            this.Close();
        }
    }
}
