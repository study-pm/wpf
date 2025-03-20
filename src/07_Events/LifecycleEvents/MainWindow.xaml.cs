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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LifecycleEvents
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            stkPnl.Initialized += StkPnl_Initialized;
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            Debug.WriteLine("Initialized", "MainWindow");
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Loaded", "MainWindow");
        }
        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            Debug.WriteLine("SourceInitialized", "MainWindow");
        }
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            Debug.WriteLine("ContentRendered", "MainWindow");
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            Debug.WriteLine("Activated", "MainWindow");
        }
        private void Window_Deactivated(object sender, EventArgs e)
        {
            Debug.WriteLine("Deactivated", "MainWindow");
        }
        private void Window_Closing(object sender, EventArgs e)
        {
            Debug.WriteLine("Closing", "MainWindow");
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Debug.WriteLine("Closed", "MainWindow");
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Unloaded", "MainWindow");
        }
        private void StkPnl_Initialized(object sender, EventArgs e)
        {
            Debug.WriteLine("Initialized", "StackPanel");
        }
        private void StkPnl_Loaded(object sender, EventArgs e)
        {
            Debug.WriteLine("Loaded", "StackPanel");
        }
        private void StkPnl_Unloaded(object sender, EventArgs e)
        {
            Debug.WriteLine("Unloaded", "StackPanel");
        }

        private void txtBx_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine("KeyDown", "TextBox");
        }
    }
}
