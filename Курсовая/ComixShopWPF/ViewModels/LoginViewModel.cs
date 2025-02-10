using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComixShopWPF.Commands.@base;
using ComixShopWPF.Data;
using ComixShopWPF.Models;
using System.Windows.Input;
using System.Windows;
using ComixShopWPF.ViewModels.Base;
using System.Security.Cryptography;
using ComixShopWPF.Views.Windows;
using ComixShopWPF.Services;
using System.Windows.Navigation;
using System.Windows.Controls;

namespace ComixShopWPF.ViewModels
{
    public class LoginViewModel:ViewModel
    {
        private string _username;
        private string _password;
        public string Username
        {
            get => _username;
            set { Set(ref _username, value); }
        }

        public string Password
        {
            get => _password;
            set { Set(ref _password, value); }
        }
        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
        }

        //private void Login(object? parm)
        //{
        //    string hashedPassword = HashPassword(Password);
            
        //    using (var context = new DataContext())
        //    {
        //        if (context.UserAccounts.Any(u => u.Username == Username && u.Password == hashedPassword))
        //        {
        //            Window window = new MainWindow();
        //            window.Show();
        //            Application.Current.MainWindow.Close();
        //            //MessageBox.Show("Логин успешен");
        //        }
        //        else
        //        {
        //            MessageBox.Show("Логин/Пароль неправильный");
        //        }
        //    }
        //}
        private void Login(object? parm)
        {
            string hashedPassword = HashPassword(Password);

            using (var context = new DataContext())
            {
                var user = context.UserAccounts
                    .FirstOrDefault(u => u.Username == Username && u.Password == hashedPassword);

                if (user != null)
                {
                    // Устанавливаем текущего пользователя в сессии
                    UserSession.Instance.Login(user);

                    // Переход на главное окно
                    var mainWindow = new MainWindow();
                    Window currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
                    mainWindow.Show();
                    currentWindow?.Close();


                    //NavigationService.Navigate(new MainWindow());
                }
                else
                {
                    MessageBox.Show("Логин/Пароль неправильный");
                }
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
