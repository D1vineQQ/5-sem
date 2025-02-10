using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComixShopWPF.Services;
using ComixShopWPF.Views.Windows;
using System.Windows;
using ComixShopWPF.Commands.@base;
using System.Windows.Input;
using ComixShopWPF.ViewModels.Base;

namespace ComixShopWPF.ViewModels.ProfileViewModels
{
    public class ProfileLogoutViewModel:ViewModel
    {
        public ICommand LogoutCommand { get; }

        public ProfileLogoutViewModel()
        {
            LogoutCommand = new RelayCommand(Logout);
        }
        private void Logout(object? parm)
        {
            UserSession.Instance.Logout();
            Window currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            var loginWindow = new AuthWindow();
            loginWindow.Show();

            currentWindow?.Close();
        }
    }
}
