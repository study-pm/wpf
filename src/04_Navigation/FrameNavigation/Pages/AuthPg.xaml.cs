﻿using System;
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

namespace FrameNavigation.Pages
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
            MessageBox.Show("Login success!");
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new System.Uri("Pages/EventsPg.xaml", UriKind.RelativeOrAbsolute));
        }

        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPg());
        }
    }
}
