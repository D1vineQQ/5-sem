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
using ComixShopWPF.Data;
using ComixShopWPF.Models;
using ComixShopWPF.ViewModels;

namespace ComixShopWPF.Views.Frames
{
    /// <summary>
    /// Логика взаимодействия для RegView.xaml
    /// </summary>
    public partial class RegView : UserControl
    {
        public RegView()
        {
            InitializeComponent();
            //DataContext = new RegViewModel();
            //Loaded += (s, e) =>
            //{
            //    if (DataContext is RegViewModel viewModel)
            //    {
            //        // Выводим в консоль для проверки
            //        MessageBox.Show("DataContext установлен: " + viewModel.GetType().Name);
            //    }
            //    else
            //    {
            //        MessageBox.Show("DataContext не установлен.");
            //    }
            //};
            //using (var context = new DataContext())
            //{
            //    UserAccount newUser = new UserAccount
            //    {
            //        Username = "Username",
            //        Password = "Password",
            //        Email = "user@example.com",
            //        FirstName = "FirstName",
            //        LastName = "LastName",
            //        PhoneNumber = "PhoneNumber",
            //        CreatedAt = DateTime.Now
            //        // Заполните другие свойства, если нужно
            //    };

            //    context.UserAccounts.Add(newUser);
            //    context.SaveChanges();
            //}
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegViewModel viewModel)
            {
                viewModel.Password = PasswordBox.Password;
            }
        }

        private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is RegViewModel viewModel)
            {
                viewModel.ConfirmPassword = ConfirmPasswordBox.Password;
            }
        }
    }
}
