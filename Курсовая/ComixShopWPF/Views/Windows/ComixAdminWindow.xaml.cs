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
using System.Windows.Shapes;
using ComixShopWPF.Models;
using ComixShopWPF.ViewModels;
using Microsoft.Win32;

namespace ComixShopWPF.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для ComixOverviewWindow.xaml
    /// </summary>
    
    public partial class ComixAdminWindow : Window
    {
        //public Comix ComixContext { get; set; }
        public ComixAdminWindow(Comix comix)
        {
            InitializeComponent();
            DataContext = new ComixAdminViewModel(comix);
        }
        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true; // Устанавливаем результат диалога
            this.Close(); // Закрываем окно
        }
        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            // Открываем диалоговое окно для выбора файла
            OpenFileDialog dialog = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "Image Files|*.jpg;*.jpeg;*.png"
            };

            if (dialog.ShowDialog() == true)
            {
                // Устанавливаем путь к изображению в модель (через DataContext)
                var viewModel = DataContext as ComixAdminViewModel;
                if (viewModel != null)
                {
                    viewModel.Comix.ImageUrl = dialog.FileName;
                }
            }
        }
    }
}
