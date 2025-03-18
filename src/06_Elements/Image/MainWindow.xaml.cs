using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace Image
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Assembly assembly = Assembly.GetExecutingAssembly();
            var AssemblyName = assembly.ToString();

            System.Windows.Controls.Image image = new System.Windows.Controls.Image();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri($"pack://application:,,,/{AssemblyName};component/Images/wpf_logo.jpg");
            bitmap.EndInit();
            image.Source = bitmap;
            image.Stretch = Stretch.Uniform;

            stkPnl.Children.Add(image);
        }
    }
}
