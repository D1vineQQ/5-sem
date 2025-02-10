using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ComixShopWPF.Commands.@base;
using ComixShopWPF.Data;
using ComixShopWPF.Models;
using ComixShopWPF.Services;
using ComixShopWPF.ViewModels.Base;
using ComixShopWPF.Views.Windows;

namespace ComixShopWPF.ViewModels
{
    
    public class RegViewModel : ViewModel
    {
        private string _username;
        private string _password;
        private string _confirmPassword;
        private string _email;

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

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set { Set(ref _confirmPassword, value); }
        }

        public string Email
        {
            get => _email;
            set { Set(ref _email, value); }
        }
        public ICommand RegisterCommand { get; }

        public RegViewModel()
        {
            RegisterCommand = new RelayCommand(Register);
        }
        private void Register(object? parm)
        {
            if (Password != ConfirmPassword)
            {
                MessageBox.Show("Пароли не совпадают!");
                return;
            }
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(Email, emailPattern))
            {
                MessageBox.Show("Некорректный формат email.");
                return;
            }
            string hashedPassword = HashPassword(Password);

            using (var context = new DataContext())
            {
                if (context.UserAccounts.Any(u => u.Username == Username || u.Email == Email))
                {
                    MessageBox.Show("Пользователь с таким именем или email уже существует!");
                    return;
                }
                var newUser = new UserAccount
                {
                    Username = Username,
                    Password = hashedPassword,
                    Email = Email,
                    RoleId = 2//user
                };

                context.UserAccounts.Add(newUser);
                context.SaveChanges();

                UserSession.Instance.Login(newUser);

                Window currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);

                var mainWindow = new MainWindow();
                mainWindow.Show();

                currentWindow?.Close();
            }
        }
        //private void Register(object? parm)
        //{
        //    if (Password != ConfirmPassword)
        //    {
        //        MessageBox.Show("Пароли не совпадают!");
        //        return;
        //    }

        //    string hashedPassword = HashPassword(Password);

        //    //UserAccount newUser = new UserAccount
        //    //{
        //    //    Username = Username,
        //    //    Password = Password,
        //    //};

        //    using (var context = new DataContext())
        //    {
        //        if (context.UserAccounts.Any(u => u.Username == Username || u.Email == Email))
        //        {
        //            MessageBox.Show("Пользователь с таким именем или email уже существует!");
        //            return;
        //        }
        //            UserAccount newUser = new UserAccount
        //        {
        //            Username = Username,
        //            Password = hashedPassword,
        //            Email = Email,
        //            //FirstName = "FirstName",
        //            //LastName = "LastName",
        //            //PhoneNumber = "PhoneNumber"
        //        };
        //        //context.UserAccounts.
        //        context.UserAccounts.Add(newUser);
        //        context.SaveChanges();
        //    }
        //    Window window = new MainWindow();
        //    window.Show();
        //    Application.Current.MainWindow.Close();
        //    //MessageBox.Show("Регистрация успешна!");
        //}

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}