using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComixShopWPF.Models;
using ComixShopWPF.ViewModels.Base;

namespace ComixShopWPF.ViewModels
{
    public class ComixAdminViewModel:ViewModel
    {
        public Comix Comix { get; set; }
        public ObservableCollection<string> CategoryList { get; set; } = new ObservableCollection<string>
        {
            "Стрип",
            "Графические романы",
            "Манхва"
        };

        public ComixAdminViewModel(Comix comix)
        {
            Comix = comix;
        }
    }
}
