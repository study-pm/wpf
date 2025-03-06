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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Task_01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Button myButton = new Button();
            myButton.Width = 100;
            myButton.Height = 30;
            myButton.Content = "Button CS";
            panel.Children.Add(myButton);
            myButton.HorizontalAlignment = HorizontalAlignment.Right;
            myButton.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
            myButton.Background = new SolidColorBrush(Colors.AliceBlue);
            myButton.Background = (Brush)System.ComponentModel.TypeDescriptor.GetConverter(typeof(Brush)).ConvertFromInvariantString("Red");
        }
    }
}
