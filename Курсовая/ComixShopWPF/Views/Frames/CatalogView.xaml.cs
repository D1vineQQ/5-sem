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
using ComixShopWPF.Models;
using ComixShopWPF.ViewModels;

namespace ComixShopWPF.Views.Frames
{
    /// <summary>
    /// Логика взаимодействия для CatalogView.xaml
    /// </summary>
    public partial class CatalogView : UserControl
    {
        public CatalogView()
        {
            InitializeComponent();
        }
        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGrid dataGrid)
            {
                var selectedProduct = dataGrid.SelectedItem as Comix;
                if (selectedProduct != null)
                {
                    var viewModel = this.DataContext as CatalogViewModel;
                    viewModel?.ComixSelectedCommand.Execute(selectedProduct);
                }
            }
        }
    }
}
