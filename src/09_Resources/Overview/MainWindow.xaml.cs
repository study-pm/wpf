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

namespace Overview
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SolidColorBrush blueColor = new SolidColorBrush(Colors.Blue);
            this.Resources.Add("staticButtonBrush", blueColor);
            staticResBtn.Background = (Brush)this.TryFindResource("staticButtonBrush");

            this.Resources.Add("dynamicButtonBrush", blueColor);
            dynamicResBtn.Background = (Brush)this.TryFindResource("dynamicButtonBrush");
        }

        private void btnClickMe_Click(object sender, RoutedEventArgs e)
        {
            lbResult.Items.Add(pnlMain.FindResource("strPanel").ToString());
            lbResult.Items.Add(this.FindResource("strWindow").ToString());
            lbResult.Items.Add(Application.Current.FindResource("strApp").ToString());

            lbResult.Items.Add(pnlMain.FindResource("strPanel").ToString());
            lbResult.Items.Add(pnlMain.FindResource("strWindow").ToString());
            lbResult.Items.Add(pnlMain.FindResource("strApp").ToString());
        }

        private void staticResBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Resources["staticButtonBrush"] = new SolidColorBrush(Colors.LimeGreen);
            this.Resources["strStatic"] = "Static Changed";
        }
        private void dynamicResBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Resources["dynamicButtonBrush"] = new SolidColorBrush(Colors.LimeGreen);
            SolidColorBrush buttonBrush = (SolidColorBrush)this.TryFindResource("dynamicButtonBrush");
            buttonBrush.Color = Colors.LimeGreen;
            this.Resources["strDynamic"] = "Dynamic Changed";
        }
    }
}
