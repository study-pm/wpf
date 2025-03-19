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

namespace RoutedEvents
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int i = 0;
        private void El_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show($"Object: {sender}, Source: {e.Source}, OriginalSource: {e.OriginalSource}, RoutedEvent: {e.RoutedEvent}");
        }
        private void El_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show($"Object: {sender}, Source: {e.Source}, OriginalSource: {e.OriginalSource}, RoutedEvent: {e.RoutedEvent}");
        }

    }
}
