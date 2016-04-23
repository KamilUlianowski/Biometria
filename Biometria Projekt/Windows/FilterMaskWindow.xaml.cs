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
    /// Interaction logic for FilterMaskWindow.xaml
    /// </summary>
    public partial class FilterMaskWindow : Window
    {
        public int[,] MaskTable = new int[3,3];

        public FilterMaskWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            MaskTable[0, 0] = Int32.Parse(TextBox00.Text);
            MaskTable[0, 1] = Int32.Parse(TextBox01.Text);
            MaskTable[0, 2] = Int32.Parse(TextBox02.Text);
            MaskTable[1, 0] = Int32.Parse(TextBox10.Text);
            MaskTable[1, 1] = Int32.Parse(TextBox11.Text);
            MaskTable[1, 2] = Int32.Parse(TextBox12.Text);
            MaskTable[2, 0] = Int32.Parse(TextBox20.Text);
            MaskTable[2, 1] = Int32.Parse(TextBox21.Text);
            MaskTable[2, 2] = Int32.Parse(TextBox22.Text);
            this.Close();
        }
    }
}
