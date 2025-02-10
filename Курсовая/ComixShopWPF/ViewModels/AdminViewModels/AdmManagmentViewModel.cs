using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComixShopWPF.Commands.@base;
using ComixShopWPF.Data;
using ComixShopWPF.Models;
using System.Windows.Input;
using ComixShopWPF.ViewModels.Base;
using ComixShopWPF.Views.Windows;

namespace ComixShopWPF.ViewModels.AdminViewModels
{
    public class AdmManagmentViewModel : CatalogViewModel
    {
        public List<string> FilterText { get; set; } = ["", "Стрип", "Графические романы", "Манхва"];
        public ObservableCollection<Comix> ComixList { get; set; } = new();
        public Comix SelectedComix { get; set; } = new();
        public ICommand AddComixCommand { get; }
        public ICommand EditComixCommand { get; }
        public ICommand DeleteComixCommand { get; }
        public AdmManagmentViewModel()
        {
            LoadComix();
            AddComixCommand = new RelayCommand(AddComix);
            EditComixCommand = new RelayCommand(EditComix);
            DeleteComixCommand = new RelayCommand(DeleteComix);
        }

        private void LoadComix()
        {
            using var context = new DataContext();
            ComixList = new ObservableCollection<Comix>(context.Comixes.ToList());
        }

        private void AddComix(object? parameter)
        {
            using var context = new DataContext();
            Comix temp = new();
            var AddWin = new ComixAdminWindow(temp);
            bool? result = AddWin.ShowDialog();
            if (result == true)
            {
                context.Comixes.Add(temp);
                context.SaveChanges();
                LoadComix();
                OnPropertyChanged(nameof(ComixList));
            }
        }

        private void EditComix(object? parameter)
        {
            if (SelectedComix == null) return;
            using var context = new DataContext();
            Comix temp = new(SelectedComix);
            var AddWin = new ComixAdminWindow(temp);
            //AddWin.ShowDialog();
            //context.Comixes.Add(SelectedComix);
            //context.SaveChanges();
            bool? result = AddWin.ShowDialog();
            if (result == true) // Условие: пользователь нажал "Подтвердить"
            {
                context.Comixes.Update(temp); // Обновляем комикс в контексте
                context.SaveChanges(); // Сохраняем изменения в базе данных
                SelectedComix = temp;
                LoadComix();
                OnPropertyChanged(nameof(ComixList));
                // Перезагружаем список комиксов
            }
        }

        private void DeleteComix(object? parameter)
        {
            if(SelectedComix == null) return;
            using var context = new DataContext();
            var comixToDelete = context.Comixes.FirstOrDefault(c => c.Id == SelectedComix.Id);
            if (comixToDelete != null)
            {
                context.Comixes.Remove(comixToDelete);
                context.SaveChanges();
                LoadComix();
                OnPropertyChanged(nameof(ComixList));
            }
        }
    }
}
