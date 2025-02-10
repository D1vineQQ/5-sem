using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ComixShopWPF.Models;
using ComixShopWPF.ViewModels.Base;

namespace ComixShopWPF.ViewModels
{
    internal class AuthWindowViewModel:ViewModel
    {
        public RegViewModel RegViewModel { get; }
        //public ObservableCollection<MenuItemViewModel> MenuItems { get; }

        //private object _selectedViewModel;

        //public object SelectedViewModel
        //{
        //    get => _selectedViewModel;
        //    set { Set(ref _selectedViewModel, value); }
        //}

        //private MenuItemViewModel _selectedItem;
        //public MenuItemViewModel SelectedItem
        //{
        //    get => _selectedItem;
        //    set
        //    {
        //        if (Set(ref _selectedItem, value))
        //        {
        //            SelectedViewModel = _selectedItem.ViewModel;
        //        }
        //    }
        //}
        //public Window win { get; set; }
        public AuthWindowViewModel()
        {
            //win = window;
            //RegViewModel = new RegViewModel();
            //MenuItems = [
            //    new MenuItemViewModel { Title="Login", Icon="", ViewModel = new AuthViewModel() },
            //    new MenuItemViewModel { Title="Register", Icon="", ViewModel = new RegisterViewModel() }
            //];
            //SelectedItem = MenuItems[0];
        }
    }
}
