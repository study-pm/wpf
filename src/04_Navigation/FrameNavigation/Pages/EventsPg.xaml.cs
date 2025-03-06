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

namespace FrameNavigation.Pages
{
    /// <summary>
    /// Interaction logic for EventsPg.xaml
    /// </summary>
    public partial class EventsPg : Page
    {
        public EventsPg()
        {
            InitializeComponent();
            Loaded += MyPage_Loaded;
        }

        private void MyPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Получение ссылки на NavigationService
            NavigationService nav = NavigationService.GetNavigationService(this);

            // Подписка на события навигации
            nav.Navigating += Nav_Navigating;
            nav.Navigated += Nav_Navigated;
            nav.NavigationFailed += Nav_NavigationFailed;
            nav.LoadCompleted += Nav_LoadCompleted;
            nav.NavigationProgress += Nav_Progress;
        }

        private void Nav_LoadCompleted(object sender, NavigationEventArgs e) => MessageBox.Show("Navigation load complete");

        private void Nav_Progress(object sender, NavigationProgressEventArgs e) =>
            MessageBox.Show($"Navigation progress: {e.BytesRead} of {e.MaxBytes} bytes total");

        private void Nav_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            // Обработка начала навигации
            MessageBox.Show("Navigation began");
        }

        private void Nav_Navigated(object sender, NavigationEventArgs e)
        {
            // Обработка завершения навигации
            MessageBox.Show("Navigation complete");
        }

        private void Nav_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            // Обработка ошибок навигации
            MessageBox.Show("Navigation error: " + e.Exception.Message);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            // Получение родительского фрейма
            Frame parentFrame = (Frame)this.Parent;

            // Если фрейм не найден, возможно, он не является прямым родителем
            if (parentFrame == null)
            {
                DependencyObject currentParent = this.Parent;
                while (currentParent != null)
                {
                    if (currentParent is Frame)
                    {
                        parentFrame = (Frame)currentParent;
                        break;
                    }
                    currentParent = LogicalTreeHelper.GetParent(currentParent);
                }
            }

            // Если фрейм найден, можно использовать его для навигации
            if (parentFrame != null)
            {
                parentFrame.Navigate(new Uri("Pages/NewPage.xaml", UriKind.Relative));
            }

            NavigationService.GoBack();
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.GoForward();
            }
            catch (Exception exc)
            {
                MessageBox.Show($"Error: {exc.Message}");
            }
        }

        private void backSafeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack) this.NavigationService.GoBack();
            else MessageBox.Show("No pages to go back!");
        }

        private void forwardBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.GoForward();
            }
            catch(Exception exc) {
                MessageBox.Show("Error: " + exc.Message);
            }
        }

        private void forwardSafeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoForward) this.NavigationService.GoForward();
            else MessageBox.Show("No pages to go forward!");
        }
    }
}
