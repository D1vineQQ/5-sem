using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComixShopWPF.Commands.@base;

namespace ComixShopWPF.Commands
{
    internal class ToCatalog : Command
    {
        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        public override void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }
    }
}
