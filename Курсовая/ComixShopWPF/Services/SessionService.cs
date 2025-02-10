using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComixShopWPF.Models;

namespace ComixShopWPF.Services
{
    public class SessionService
    {
        public UserAccount CurrentUser { get; private set; }
        public void Login(UserAccount user)
        {
            CurrentUser = user;
        }
        public void Logout()
        {
            CurrentUser = null;
        }
        public bool IsAuthenticated => CurrentUser != null;
    }
}
