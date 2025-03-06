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
    /// Interaction logic for RegisterPg.xaml
    /// </summary>
    public partial class RegisterPg : Page
    {
        public RegisterPg()
        {
            InitializeComponent();
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
            else
            {
                MessageBox.Show("No page to go back!");
            }
        }

        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Registration Success!");
            NavigationService.Navigate(new System.Uri("Pages/EventsPg.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
