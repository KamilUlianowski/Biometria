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
using Biometria_Projekt.Annotations;

namespace Biometria_Projekt.Windows
{
    /// <summary>
    /// Interaction logic for MaskWindow.xaml
    /// </summary>
    public partial class MaskWindow : Window
    {
        public int MaskSize;
        public MaskWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void ButtonConfirm_OnClick(object sender, RoutedEventArgs e)
        {
            Int32.TryParse(TextBlockMaskSize.Text, out MaskSize);
            this.Close();
        }
    }
}
