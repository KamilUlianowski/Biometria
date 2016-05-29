using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Biometria_Projekt.Classes;

namespace Biometria_Projekt.Windows
{
    /// <summary>
    /// Interaction logic for IdentityUserWindow.xaml
    /// </summary>
    public partial class IdentityUserWindow : Window
    {
        private int numberOfSamples;
        private int count = 0;
        private int numberOfTest = 0;
        public string[] Texts { get; set; } 
        public Stopwatch StopWatcher = new Stopwatch();
        private bool started { get; set; }

        public IdentityUserWindow()
        {
            InitializeComponent();
        }

        private void UserText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (started == false)
            {
                started = true;
                StopWatcher.Start();
            }
            if (numberOfSamples > 0 && numberOfSamples == UserText.Text.Length)
            {
                MessageBox.Show("Dziękuje za wpisanie tekstu");
                StopWatcher.Stop();
                StopWatcher.Reset();
                started = false;
                numberOfTest++;
                StackPanelSamples.IsEnabled = true;
                UserText.Text = string.Empty;
                if (numberOfTest == Int32.Parse(NumberOfTests.Text))
                {
                    this.Close();
                }
            }

        }



        private void UserText_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (started == false)
            {
                started = true;
                StopWatcher.Start();
            }
            count++;
            long time = StopWatcher.ElapsedMilliseconds;
            char key = Keystroke.GetCharFromKey(e.Key);
            Texts[numberOfTest] += "d" + "_" + (int)key + "_" + time + "_" + count + " ";
        }

        private void UserText_OnKeyUp(object sender, KeyEventArgs e)
        {
            long time = StopWatcher.ElapsedMilliseconds;
            char key = Keystroke.GetCharFromKey(e.Key);
            Texts[numberOfTest] += "u" + "_" + (int)key + "_" + time + " ";
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            StackPanelTests.IsEnabled = false;
        }

        private void ConfirmButtonSamples_OnClick(object sender, RoutedEventArgs e)
        {
            Texts = new string[3];
            numberOfSamples = Int32.Parse(NumberOfSamples.Text);
            StackPanelSamples.IsEnabled = false;
        }
    }
}
