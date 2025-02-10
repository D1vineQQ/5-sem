using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComixShopWPF.Commands.@base;
using ComixShopWPF.Data;
using ComixShopWPF.Models;
using ComixShopWPF.Services;
using ComixShopWPF.ViewModels.Base;
using System.Windows.Input;
using System.Windows;
using System.Text.RegularExpressions;

namespace ComixShopWPF.ViewModels.ProfileViewModels
{
    public class ProfileInfoViewModel : ViewModel
    {
        private UserAccount _userAccount;
        public UserAccount CurrentUser { get => _userAccount; set { Set(ref _userAccount, value); } }
        public ICommand SaveProfileCommand { get; }
        public ICommand LoadProfileCommand { get; }
        public ProfileInfoViewModel()
        {
            LoadProfile(null);
            SaveProfileCommand = new RelayCommand(SaveProfile);
            LoadProfileCommand = new RelayCommand(LoadProfile);
        }
        private void LoadProfile(object? parm)
        {
            using (var context = new DataContext())
            {
                var userId = UserSession.Instance.CurrentUser.Id;
                CurrentUser = context.UserAccounts.SingleOrDefault(u => u.Id == userId);
            }
        }
        private void SaveProfile(object? parm)
        {
            using (var context = new DataContext())
            {
                var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";  // Простой паттерн для email

                if (!Regex.IsMatch(CurrentUser.Email, emailPattern))
                {
                    MessageBox.Show("Некорректный email адрес.");
                    return;
                }
                if (context.UserAccounts.Any(u => u.Email == CurrentUser.Email && u.Id != CurrentUser.Id))
                {
                    MessageBox.Show("Пользователь с таким email уже существует.");
                    return;
                }
                var userInDb = context.UserAccounts.SingleOrDefault(u => u.Id == CurrentUser.Id);
                if (userInDb != null)
                {
                    userInDb.Email = CurrentUser.Email;
                    userInDb.FirstName = CurrentUser.FirstName;
                    userInDb.LastName = CurrentUser.LastName;
                    //userInDb.PhoneNumber = CurrentUser.PhoneNumber;

                    context.SaveChanges();
                    MessageBox.Show("Профиль успешно обновлен!");
                }
                else
                {
                    MessageBox.Show("Ошибка: пользователь не найден.");
                }
            }
        }
    }
}
