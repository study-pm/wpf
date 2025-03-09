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

namespace Thumb
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
        private void Thumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            // Обработка начала перетаскивания
            MessageBox.Show("Перетаскивание начато");
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            // Обработка изменения положения Thumb
            System.Windows.Controls.Primitives.Thumb thumb = (System.Windows.Controls.Primitives.Thumb)sender;
            Canvas.SetLeft(thumb, Canvas.GetLeft(thumb) + e.HorizontalChange);
            Canvas.SetTop(thumb, Canvas.GetTop(thumb) + e.VerticalChange);
            MessageBox.Show($"Thumb перемещен на ({e.HorizontalChange}, {e.VerticalChange})");
        }

        private void Thumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            // Обработка окончания перетаскивания
            MessageBox.Show("Перетаскивание завершено");
        }
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Message != null)
                Message.FontSize = ((Slider)sender).Value;
        }
    }
}
