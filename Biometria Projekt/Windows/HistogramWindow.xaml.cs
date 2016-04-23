using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Biometria_Projekt.Windows;

namespace Biometria_Projekt
{
    /// <summary>
    /// Interaction logic for HistogramWindow.xaml
    /// </summary>
    public partial class HistogramWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public PointCollection luminanceHistogramPoints = null;
        public PointCollection redColorHistogramPoints = null;
        public PointCollection greenColorHistogramPoints = null;
        public PointCollection blueColorHistogramPoints = null;
        public bool PerformHistogramSmoothing { get; set; }
        public byte[] changePixels;


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public PointCollection LuminanceHistogramPoints
        {
            get { return this.luminanceHistogramPoints; }
            set
            {
                this.luminanceHistogramPoints = value;
                this.OnPropertyChanged("LuminanceHistogramPoints");
            }
        }

        public PointCollection RedColorHistogramPoints
        {
            get { return this.redColorHistogramPoints; }
            set
            {
                if (this.redColorHistogramPoints != value)
                {
                    this.redColorHistogramPoints = value;
                    this.OnPropertyChanged("RedColorHistogramPoints");
                }
            }
        }

        public PointCollection GreenColorHistogramPoints
        {
            get { return this.greenColorHistogramPoints; }
            set
            {
                this.greenColorHistogramPoints = value;
                this.OnPropertyChanged("GreenColorHistogramPoints");
            }
        }

        public PointCollection BlueColorHistogramPoints
        {
            get { return this.blueColorHistogramPoints; }
            set
            {

                this.blueColorHistogramPoints = value;
                this.OnPropertyChanged("BlueColorHistogramPoints");

            }
        }

        public HistogramWindow(byte[] pix)
        {
            InitializeComponent();
            this.DataContext = this;
            this.changePixels = pix;
            Start();
        
        }

        public void Start()
        {
           
                this.LuminanceHistogramPoints = ConvertToPointCollection(GetHistogramAverageVaues());
                this.RedColorHistogramPoints = ConvertToPointCollection(GetHistogramRedValues());
                this.GreenColorHistogramPoints = ConvertToPointCollection(GetHistogramGreenValues());
                this.BlueColorHistogramPoints = ConvertToPointCollection(GetHistogramBlueValues());
            
        }

        private PointCollection ConvertToPointCollection(int[] values)
        {
            int max = values.Max();
            PointCollection points = new PointCollection();
            points.Add(new Point(0, max));
            for (int i = 0; i < values.Length; i++)
            {
                points.Add(new Point(i, max - values[i]));
            }
            points.Add(new Point(values.Length - 1, max));
            return points;
        }

        public int[] GetHistogramAverageVaues()
        {
            int[] averageValues = new int[256];
            for (int i = 0; i < changePixels.Length; i += 4)
            {
                int value = (changePixels[i] + changePixels[i + 1] + changePixels[i + 2]) / 3;
                averageValues[value]++;
            }

            return averageValues;

        }

        public int[] GetHistogram()
        {
            int[] histogram = new int[256];

            for (int i = 0; i < changePixels.Length; i++)
            {
                histogram[changePixels[i]]++;
            }
            return histogram;
        }

        public int[] GetHistogramRedValues()
        {
            int[] redValues = new int[256];

            for (int i = 2; i < changePixels.Length; i += 4)
            {
                redValues[changePixels[i]]++;
            }

            return redValues;
        }

        public int[] GetHistogramGreenValues()
        {
            int[] greenValues = new int[256];

            for (int i = 1; i < changePixels.Length; i += 4)
            {
                greenValues[changePixels[i]]++;
            }

            return greenValues;
        }

        public int[] GetHistogramBlueValues()
        {
            int[] blueValues = new int[256];

            for (int i = 0; i < changePixels.Length; i += 4)
            {
                blueValues[changePixels[i]]++;
            }

            return blueValues;
        }


    }
}

