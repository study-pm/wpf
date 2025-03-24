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

namespace RoutedEvents
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            textBlk.MouseUp += Element_MouseUp;
            textBlk.MouseUp -= Element_MouseUp;

            txtBx.TabIndex = 0;
        }

        private void Element_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show($"Object: {sender}, Source: {e.Source}, OriginalSource: {e.OriginalSource}, RoutedEvent: {e.RoutedEvent}");
        }
        private void Element_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show($"Object: {sender}, Source: {e.Source}, OriginalSource: {e.OriginalSource}, RoutedEvent: {e.RoutedEvent}");
        }

        int i = 0;

        private void El_MouseUp(object sender, MouseButtonEventArgs e)
        {
            i++;
            string message = "--> " + i + ":\r\n" +
                "Объект: " + sender.ToString() + "\r\n" +
                "Событие: " + e.RoutedEvent.ToString() + "\r\n" +
                "Источник: " + e.Source.ToString() + "\r\n" +
                "Первоначальный источник: " + e.OriginalSource + "\r\n" +
                "Состояние кнопки: " + e.ButtonState + "\r\n" +
                "Кнопка: " + e.ChangedButton + "\r\n" +
                "Число нажатий: " + e.ClickCount + "\r\n" +
                "ЛКМ: " + e.LeftButton + "\r\n" +
                "СКМ: " + e.MiddleButton + "\r\n" +
                "ПКМ: " + e.RightButton + "\r\n"
            ;
            lbInfoBubble.Items.Add(message);
            e.Handled = (bool)chb_ShowFirstEvent.IsChecked;
        }
        private void El_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            i++;
            string message = "--> " + i + ":\r\n" +
                "Объект: " + sender.ToString() + "\r\n" +
                "Событие: " + e.RoutedEvent.ToString() + "\r\n" +
                "Источник: " + e.Source.ToString() + "\r\n" +
                "Первоначальный источник: " + e.OriginalSource + "\r\n" +
                "Состояние кнопки: " + e.ButtonState + "\r\n" +
                "Кнопка: " + e.ChangedButton + "\r\n" +
                "Число нажатий: " + e.ClickCount + "\r\n" +
                "ЛКМ: " + e.LeftButton + "\r\n" +
                "СКМ: " + e.MiddleButton + "\r\n" +
                "ПКМ: " + e.RightButton + "\r\n"
            ;
            lbInfoTunnel.Items.Add(message);
            // This locks the tunneling sequence thus button stops working
            // e.Handled = (bool)chb_ShowFirstEvent.IsChecked;
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            lbInfoBubble.Items.Clear();
            lbInfoTunnel.Items.Clear();
            i = 0;
            txtBx.Text = string.Empty;
        }

        private void DoSomething(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource == opt1)
            { }
            else if (e.OriginalSource == opt2)
            { }
            else if (e.OriginalSource == opt3)
            { }
        }
        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton selectedRadio = (RadioButton)e.Source;
            textBlock1.Text = "Вы выбрали: " + selectedRadio.Content.ToString();
        }
        private void txtBx_KeyEvt(object sender, KeyEventArgs e)
        {
            if ((bool)IgnoreRepeatChkBx.IsChecked && e.IsRepeat) return;
            i++;
            string msg = "--> " + i + ":\r\n" +
                "Событие: " + e.RoutedEvent + "\r\n" +
                "Клавиша: " + e.Key + "\r\n" +
                "Системная клавиша: " + e.SystemKey + "\r\n" +
                "Повтор: " + e.IsRepeat + "\r\n" +
                "Нажатие: " + e.IsDown + "\r\n" +
                "Освобождение: " + e.IsUp + "\r\n" +
                "Включение: " + e.IsToggled + "\r\n" +
                "Модификаторы: " + e.KeyboardDevice.Modifiers.ToString() + "\r\n" +
                "Элемент фокуса: " + e.KeyboardDevice.FocusedElement.ToString()
            ;
            lbInfoBubble.Items.Add(msg);
        }
        private void txtBx_TextInput(object sender, TextCompositionEventArgs e)
        {
            // Locked: Never fires, being suppressed by TextBox
            MessageBox.Show("TextInput");
        }
        private void txtBx_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            i++;
            string msg = "--> " + i + ":\r\n" +
                "Событие: " + e.RoutedEvent + "\r\n" +
                "Текст: " + e.Text
            ;
            lbInfoTunnel.Items.Add(msg);
        }
        private void txtBx_GotFocus(object sender, RoutedEventArgs e)
        {
            i++;
            string msg = "--> " + i + ":\r\n" +
                "Событие: " + e.RoutedEvent + "\r\n"
            ;

            lbInfoBubble.Items.Add(msg);
            Debug.WriteLine("GotFocus");
            // txtBx.SelectAll();
            txtBx.SelectionStart = 0;
            txtBx.SelectionLength = txtBx.Text.Length;
        }
        private void txtBx_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            i++;
            string msg = "--> " + i + ":\r\n" +
                "Событие: " + e.RoutedEvent + "\r\n"
            ;
            lbInfoTunnel.Items.Add(msg);

            Debug.WriteLine("PreviewMouseDown");
            if (!txtBx.IsKeyboardFocusWithin)
            {
                e.Handled = true;
                txtBx.Focus();
                Debug.WriteLine("Focus");
            }
        }

        private void StackPanel_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(this);
            lbInfoBubble.Items.Add($"Позиция: {p.X} {p.Y}");
        }

        private void clearBtn_Drop(object sender, DragEventArgs e)
        {
            var btn = (Button)sender;

            if (e.Data.GetDataPresent(typeof(string)))
            {
                e.Effects = DragDropEffects.Copy; // Установка эффекта
                // Вставьте текст в целевой элемент
                btn.Content = e.Data.GetData(DataFormats.Text);
                // Alternative solution
                string text = (string)e.Data.GetData(typeof(string));
                btn.Content = text;
            }
        }

        #region Navigation Events
        private void Window_Initialized(object sender, EventArgs e)
        {
            Debug.WriteLine("Initialized", "MainWindow");
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Loaded", "MainWindow");
        }
        private void Window_SourceInitialized(object sender, EventArgs e)
        {
            Debug.WriteLine("SourceInitialized", "MainWindow");
        }
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            Debug.WriteLine("ContentRendered", "MainWindow");
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            Debug.WriteLine("Activated", "MainWindow");
        }
        private void Window_Deactivated(object sender, EventArgs e)
        {
            Debug.WriteLine("Deactivated", "MainWindow");
        }
        private void Window_Closing(object sender, EventArgs e)
        {
            Debug.WriteLine("Closing", "MainWindow");
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            Debug.WriteLine("Closed", "MainWindow");
        }
        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Unloaded", "MainWindow");
        }
        private void StkPnl_Initialized(object sender, EventArgs e)
        {
            Debug.WriteLine("Initialized", "StackPanel");
        }
        private void StkPnl_Loaded(object sender, EventArgs e)
        {
            Debug.WriteLine("Loaded", "StackPanel");
        }
        private void StkPnl_Unloaded(object sender, EventArgs e)
        {
            Debug.WriteLine("Unloaded", "StackPanel");
        }
        #endregion
    }
}
