using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RoutedEvents
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_Startup(object sender, StartupEventArgs e)
        {
            Debug.WriteLine("Startup", "Application");
        }
        private void App_Exit(object sender, ExitEventArgs e)
        {
            Debug.WriteLine("Exit", "Application");
        }
        private void App_SessionEnding(object sender, EventArgs e)
        {
            Debug.WriteLine("SessionEnding", "Application");
        }
        private void App_DispatcherUnhandledException(object sender, EventArgs e)
        {
            Debug.WriteLine("DispatcherUnhandledException", "Application");
        }
    }
}
