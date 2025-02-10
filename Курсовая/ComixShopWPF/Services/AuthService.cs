using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComixShopWPF.Data;
using ComixShopWPF.Models;
using Microsoft.EntityFrameworkCore;

namespace ComixShopWPF.Services
{
    public class AuthService
    {
        //private readonly DataContext _context;
        //private readonly SessionService _sessionService;

        //public AuthService(DataContext context, SessionService sessionService)
        //{
        //    _context = context;
        //    _sessionService = sessionService;
        //}

        ///// <summary>
        ///// Вход пользователя. Проверяет имя и пароль.
        ///// </summary>
        //public async Task<bool> LoginAsync(string username, string password)
        //{
        //    // Хешируем пароль перед сравнением
        //    string hashedPassword = HashPassword(password);

        //    // Поиск пользователя
        //    var user = await _context.UserAccounts
        //        .FirstOrDefaultAsync(u => u.Username == username && u.Password == hashedPassword);

        //    if (user != null)
        //    {
        //        // Сохраняем текущего пользователя в сессии
        //        _sessionService.Login(user);
        //        return true;
        //    }

        //    return false;
        //}

        //private string HashPassword(string password)
        //{
        //    // Пример простого хеша пароля, лучше использовать защищённые хеши
        //    using (var sha256 = System.Security.Cryptography.SHA256.Create())
        //    {
        //        var bytes = System.Text.Encoding.UTF8.GetBytes(password);
        //        var hash = sha256.ComputeHash(bytes);
        //        return Convert.ToBase64String(hash);
        //    }
        //}



        //public async Task<bool> RegisterAsync(string username, string email, string password, string confirmPassword)
        //{
        //    if (password != confirmPassword)
        //    {
        //        throw new InvalidOperationException("Пароли не совпадают!");
        //    }

        //    string hashedPassword = HashPassword(password);

        //    // Проверяем, есть ли пользователь с таким именем или email
        //    if (await _context.UserAccounts.AnyAsync(u => u.Username == username || u.Email == email))
        //    {
        //        throw new InvalidOperationException("Пользователь с таким именем или email уже существует!");
        //    }

        //    var newUser = new UserAccount
        //    {
        //        Username = username,
        //        Password = hashedPassword,
        //        Email = email,
        //        CreatedAt = DateTime.Now
        //    };

        //    _context.UserAccounts.Add(newUser);
        //    await _context.SaveChangesAsync();

        //    // Сохраняем пользователя в сессии
        //    _sessionService.Login(newUser);

        //    return true;
        //}
    }
}
