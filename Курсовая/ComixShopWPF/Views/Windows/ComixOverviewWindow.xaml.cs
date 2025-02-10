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

namespace ComixShopWPF.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для ComixOverviewWindow.xaml
    /// </summary>
    public partial class ComixOverviewWindow : Window
    {
        public ComixOverviewWindow(Comix comix)
        {
            InitializeComponent();
            DataContext = comix;
        }
    }
}
