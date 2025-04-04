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

namespace RoleNavigation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool canGoBack {
            get {
                return App.mainFrame.NavigationService.CanGoBack;
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            App.mainFrame = mainFrm;
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            if (App.mainFrame.NavigationService.CanGoBack)
            {
                App.mainFrame.NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("No page to go back!");
            }
        }

        private void forwardBtn_Click(object sender, RoutedEventArgs e)
        {
            if (App.mainFrame.NavigationService.CanGoForward)
            {
                App.mainFrame.NavigationService.GoForward();
            }
            else
            {
                MessageBox.Show("No page to go forward!");
            }
        }
    }
}
