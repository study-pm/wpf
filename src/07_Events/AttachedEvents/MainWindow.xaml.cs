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

namespace AttachedEvents
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
        private void DoSomething(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource == cmd1)
            { }
            else if (e.OriginalSource == cmd2)
            { }
            else if (e.OriginalSource == cmd3)
            { }
        }
        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton selectedRadio = (RadioButton)e.Source;
            textBlock1.Text = "Вы выбрали: " + selectedRadio.Content.ToString();
        }
    }
}
