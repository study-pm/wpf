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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public partial class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Person(string firstName, string lastName) {
                FirstName = firstName;
                LastName = lastName;
            }
            public Person(string lastName) : this(string.Empty, lastName)
            { }
            public Person() : this("Anonymous")
            { }
            public override string ToString()
            {
                return FirstName + " " + LastName;
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            double n = 12.36;
            numBound.Content = n;

            // Person person = new Person("Tom", "Smith");
            // Person person = new Person("Smith");
            // Person person = new Person();
            // Person person = new Person { FirstName = "Tom" };
            Person person = new Person { FirstName = "Nick", LastName = "Holmes" };
            objBound.Content = person;

            Button btn = new Button();
            btn.Content = new Button { Content = "Programmatically Created Buttons" };
            stack.Children.Add(btn);

            StackPanel stackPnl = new StackPanel();
            stackPnl.Children.Add(new Label { Content = "Button Set" });
            stackPnl.Children.Add(new Button { Content = "Pro Button 1", Background = new SolidColorBrush(Colors.Red), Height = 30 });
            stackPnl.Children.Add(new Button { Content = "Pro Button 2", Background = new SolidColorBrush(Colors.Blue), Height = 30 });
            stackPnl.Children.Add(new Button { Content = "Pro Button 3", Background = new SolidColorBrush(Colors.Green), Height = 30 });
            Button buttonSet = new Button { Content = stackPnl };
            stack.Children.Add(buttonSet);

            scrollV.IsDeferredScrollingEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void acceptBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("OK");
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Toggle Button IsChecked: " + (sender as System.Windows.Controls.Primitives.ToggleButton).IsChecked);
        }
    }
}
