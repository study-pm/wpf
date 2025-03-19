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

namespace EventsRouting
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
            i++;
            string message = "--> " + i + ":\r\n" +
                "Объект: " + sender.ToString() + "\r\n" +
                "Источник: " + e.Source.ToString() + "\r\n" +
                "Начальный источник: " + e.OriginalSource;
            lbInfoBubble.Items.Add(message);
            e.Handled = (bool)chb_ShowFirstEvent.IsChecked;
        }
        private void El_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            i++;
            string message = "--> " + i + ":\r\n" +
                "Объект: " + sender.ToString() + "\r\n" +
                "Источник: " + e.Source.ToString() + "\r\n" +
                "Начальный источник: " + e.OriginalSource;
            lbInfoTunnel.Items.Add(message);
            // This locks the tunneling sequence thus button stops working
            // e.Handled = (bool)chb_ShowFirstEvent.IsChecked;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            lbInfoBubble.Items.Clear();
            lbInfoTunnel.Items.Clear();
            i = 0;
        }
    }
}
