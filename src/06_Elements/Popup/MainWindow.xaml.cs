using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Popup
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
        private void Button_MouseEnter_1(object sender, MouseEventArgs e)
        {
            popup1.IsOpen = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            myPopup.IsOpen = true;
            myPopup.HorizontalOffset = 100;
            myPopup.VerticalOffset = 100;
            myPopup.AllowsTransparency = true;
            myPopup.PopupAnimation = System.Windows.Controls.Primitives.PopupAnimation.Fade;
        }

        private void myBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            myPopup.IsOpen= false;
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            // Создание таймера
            DispatcherTimer timer = new DispatcherTimer();

            // Настройка таймера
            timer.Interval = TimeSpan.FromSeconds(3); // Скрыть через 5 секунд
            timer.Tick += (s, args) =>
            {
                addedPopup.IsOpen = false; // Скрыть Popup
                timer.Stop(); // Остановить таймер
            };

            // Запуск таймера при открытии Popup
            addedPopup.IsOpen = true;
            timer.Start();
        }
    }
}
