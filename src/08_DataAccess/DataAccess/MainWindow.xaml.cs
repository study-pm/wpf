using DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
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
using System.Configuration;

namespace DataAccess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Создать контекст базы данных
            UsersEntities ctx = new UsersEntities();

            // Настройка логирования запросов
            // ctx.Database.Log = (s => System.Diagnostics.Debug.WriteLine(s, "Request"));
            ctx.Database.Log = (s => Console.WriteLine(s));

            ObjectContext context = (ctx as IObjectContextAdapter).ObjectContext;

            // Создать объект подключения и команду
            SqlConnection connection = new SqlConnection(
                @"Data Source=(localdb)\mssqllocaldb;Initial Catalog=Users");

            // Получить строку подключения из файла конфигурации
            string connectionString = ConfigurationManager.ConnectionStrings["UsersEntities"].ConnectionString;
            connection = new SqlConnection(connectionString
                .Split(';')
                .FirstOrDefault(c => c.StartsWith("provider connection string="))
                .Replace("provider connection string=", "")
            .Replace("\"", ""));

            // Создать запрос
            ObjectQuery<DbDataRecord> Users =
                context.CreateQuery<DbDataRecord>("SELECT u.Name FROM Users AS u");
            
            // Отобразить имя первого пользователя в таблице Users
            Debug.WriteLine(Users != null ? Users.First()["Name"].ToString() : "Таблица пустая", "Entity SQL");
            if (Users != null && Users.Any())
            {
                Debug.WriteLine(Users.First()["Name"].ToString());
                Console.WriteLine("IF");
            }
            else
            {
                Debug.WriteLine("Таблица пустая");
            }

            // Using LINQ
            var role = ctx.Roles
                .Select(c => c.Title)
                .FirstOrDefault();

            MessageBox.Show(role);

            var users = from u in ctx.Users
                        where u.Name.StartsWith("s")
                        orderby u.Name
                        select u;

            foreach (var user in users)
                Debug.WriteLine("Query: " + user.Name);

            users = ctx.Users.Where(u => u.Name.StartsWith("s")).OrderBy(u => u.Name);
            users = ctx.Users.Select(u => u).Where(u => u.Name.StartsWith("s")).OrderBy(u => u.Name);

            foreach (var user in users)
                Debug.WriteLine("Method: " + user.Name);

            var userCount = (from u in ctx.Users select u).Count();
            Debug.WriteLine("Total users: " + userCount.ToString());

            User usr = (from u in ctx.Users
                       where u.Name == "john"
                       select u).FirstOrDefault();

            usr.Name = "John";

            usr = ctx.Users.Where(u => u.Name == "john").FirstOrDefault();
            usr.Name = "John";
            ctx.SaveChanges();
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UsersEntities ctx = new UsersEntities();
                ctx.Users.Add(new User()
                {
                    Name = "Вася",
                    RoleId = 2
                });

                ctx.SaveChanges();
                
                MessageBox.Show("User successfully added");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
