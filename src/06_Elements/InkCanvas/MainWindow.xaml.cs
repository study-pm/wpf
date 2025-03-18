using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InkCanvas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            inkCanvas.Width = 200;
            inkCanvas.Height = 200;

            // Получение текущих атрибутов рисования по умолчанию
            DrawingAttributes drawingAttributes = inkCanvas.DefaultDrawingAttributes;

            // Изменение толщины штриха
            drawingAttributes.Width = 5; // Установка толщины в 5 пикселей

            // Изменение цвета штриха
            drawingAttributes.Color = Colors.Red; // Установка цвета в красный

            // Применение изменений
            inkCanvas.DefaultDrawingAttributes = drawingAttributes;
        }
        private void Clear()
        {
            eraseByPointBtn.IsEnabled = false;
            eraseByStrokeBtn.IsEnabled = false;
            clearBtn.IsEnabled = false;
            inkBtn.IsChecked = true;
            inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
        }
        private void inkBtn_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
        }
        private void eraseByPointBtn_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
        }
        private void eraseByStrokeBtn_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
        }
        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas.Strokes.Clear();
            Clear();
        }

        private void inkCanvas_StrokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            eraseByPointBtn.IsEnabled = true;
            eraseByStrokeBtn.IsEnabled = true;
            clearBtn.IsEnabled = true;
        }

        private void inkCanvas_StrokeErased(object sender, RoutedEventArgs e)
        {
            if (inkCanvas.Strokes.Count > 0) return;
            Clear();
        }
    }
}
