using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ComixShopWPF.ViewModels.Base
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public virtual bool Set<T>(ref T item, T value, [CallerMemberName] string propertyName = "")
        {
            if(Equals(item, value)) return false;
            item = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
