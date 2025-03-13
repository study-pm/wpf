using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ItemsControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Person> people = new List<Person>()
        {
            new Person{Name="Tom", Age=38},
            new Person {Name="Bob", Age=42},
            new Person{Name="Sam", Age=25}
        };
        public ObservableCollection<Phone> Phones { get; set; }

        ObservableCollection<string> items = new ObservableCollection<string> { "Item1", "Item2", "Item3" };
        public MainWindow()
        {
            InitializeComponent();

            usersList.Items.Remove("Sam");      // удаляем элемент "Sam"
            usersList.Items.RemoveAt(1);        // удаляем второй элемент
            usersList.Items.Add("Kate");        // Добавляем элемент "Kate"
            usersList.Items.Insert(0, "Mike");  // Вставляем элемент "Mike" на первое место в списке
            // MessageBox.Show(usersList.Items.Count.ToString());
            usrList.ItemsSource = people;
            // устанавливаем отображаемое свойство
            usrList.DisplayMemberPath = "Name";

            System.Windows.Controls.ItemsControl itemsControl = new System.Windows.Controls.ItemsControl();
            itemsControl.ItemsSource = items;
            pane1.Children.Add(itemsControl);
            Button addBtn = new Button { Content = "Add" };
            addBtn.Click += AddItem;
            pane1.Children.Add(addBtn);
            Button removeBtn = new Button { Content = "Remove" };
            removeBtn.Click += RemoveItem;
            pane1.Children.Add(removeBtn);
            Button clearBtn = new Button { Content = "Clear" };
            clearBtn.Click += RemoveAll;
            pane1.Children.Add(clearBtn);

            Phones = new ObservableCollection<Phone>
            {
                new Phone { Title = "iPhone 6S", Company = "Apple", Price = 54990 },
                new Phone { Title = "Lumia 950", Company = "Microsoft", Price = 39990 },
                new Phone { Title = "Samsung Galaxy A14", Company = "Samsung", Price = 14990 },
            };
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            people.Add(new Person { Name = "Mike", Age = 29 });
            usrList.Items.Refresh();
        }
        private void AddItem(object sender, RoutedEventArgs e)
        {
            items.Add("NewItem");
        }
        private void RemoveItem(object sender, RoutedEventArgs e)
        {
            if (items.Count > 0) items.RemoveAt(items.Count - 1);
        }
        private void RemoveAll(object sender, RoutedEventArgs e)
        {
            items.Clear();
        }
        private void usersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstBx.SelectedItem is Person selectedPerson && lstBx.SelectedValue is string selectedName)
            {
                MessageBox.Show($"Выбран элемент {selectedPerson} со значением выбора {selectedName}");
            }
        }
        private void cmButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ContextMenu cm = (ContextMenu)cmButton.ContextMenu;
                cm.PlacementTarget = cmButton;
                cm.IsOpen = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
    public class Person
    {
        public string Name { get; set; } = "";
        public int Age { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}  Age: {Age}";
        }
    }
    public class Phone
    {
        public string Title { get; set; }
        public string Company { get; set; }
        public int Price { get; set; }
    }
}
