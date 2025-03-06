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

namespace PageNavigation.Pages
{
    /// <summary>
    /// Interaction logic for AuthPg.xaml
    /// </summary>
    public partial class AuthPg : Page
    {
        public AuthPg()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Login");
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationWindow win = (NavigationWindow)Window.GetWindow(this);
            // Alternative way
            // ((Window)this.Parent).Close();

            win.Close();
            // win.Hide();
            // win.Show();

        }

        private void openBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationWindow win = new NavigationWindow();
            win.Content = new ProductPg();
            win.Show();
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Click!");
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            // Открываем ссылку в системном браузере
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            // Указываем, что событие обработано
            e.Handled = true;
        }

        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.NavigationService.Navigate(new RegisterPg());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
