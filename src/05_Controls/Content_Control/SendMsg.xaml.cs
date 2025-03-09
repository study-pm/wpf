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

namespace Content_Control
{
    /// <summary>
    /// Interaction logic for SendMsg.xaml
    /// </summary>
    public partial class SendMsg : UserControl
    {
        public SendMsg()
        {
            InitializeComponent();
        }
        private void sendButton_Click(object sender, RoutedEventArgs e)
        {
            // Обработка нажатия кнопки
            string message = messageTextBox.Text;
            MessageBox.Show($"Сообщение отправлено: {message}");
        }
    }
}
