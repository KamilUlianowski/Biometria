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
using System.Windows.Controls.DataVisualization.Charting;

namespace Biometria_Projekt.Windows
{
    /// <summary>
    /// Interaction logic for ChartHistogramWindow.xaml
    /// </summary>
    public partial class ChartHistogramWindow : Window
    {
        public ChartHistogramWindow(int[] tabAverage, int[] tabRed, int[] tabGreen, int[] tabBlue)
        {
            InitializeComponent();
            showColumnChart(tabAverage, tabRed, tabGreen, tabBlue);

        }

        public void showColumnChart(int[] tabAverage, int[] tabRed, int[] tabGreen, int[] tabBlue)
        {
            List<KeyValuePair<int, int>> valueListAverage = new List<KeyValuePair<int, int>>();
            List<KeyValuePair<int, int>> valueListRed = new List<KeyValuePair<int, int>>();
            List<KeyValuePair<int, int>> valueListGreen = new List<KeyValuePair<int, int>>();
            List<KeyValuePair<int, int>> valueListBlue = new List<KeyValuePair<int, int>>();

            for (int i = 0; i < 256; i++)
            {
                valueListAverage.Add(new KeyValuePair<int, int>(i, tabAverage[i]));
            }
             LineGrey.DataContext = valueListAverage;

            for (int i = 0; i < 256; i++)
            {
                valueListRed.Add(new KeyValuePair<int, int>(i, tabRed[i]));
            }
            areaChartRed.DataContext = valueListRed;

            for (int i = 0; i < 256; i++)
            {
                valueListGreen.Add(new KeyValuePair<int, int>(i, tabGreen[i]));
            }
            areaChartGreen.DataContext = valueListGreen;

            for (int i = 0; i < 256; i++)
            {
                valueListBlue.Add(new KeyValuePair<int, int>(i, tabBlue[i]));
            }
            areaChartBlue.DataContext = valueListBlue;

        }

        private static Style GetNewDataPointStyle(int r, int g, int b)
        {
            Color background = Color.FromRgb((byte)r,
                                             (byte)g,
                                             (byte)b);
            Style style = new Style(typeof(DataPoint));
            Setter st1 = new Setter(DataPoint.BackgroundProperty,
                                        new SolidColorBrush(background));
            Setter st2 = new Setter(DataPoint.BorderBrushProperty,
                                        new SolidColorBrush(System.Windows.Media.Colors.White));
            Setter st3 = new Setter(DataPoint.BorderThicknessProperty, new Thickness(0.1));

            Setter st4 = new Setter(DataPoint.TemplateProperty, null);
            style.Setters.Add(st1);
            style.Setters.Add(st2);
            style.Setters.Add(st3);
            style.Setters.Add(st4);
            return style;
        }
    }
}

