using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComixShopWPF.Data;
using System.Windows;
using ComixShopWPF.Models;

namespace ComixShopWPF.Services
{
    public class UserSession
    {
        private static UserSession? _instance;
        public UserAccount? CurrentUser { get; private set; }

        private UserSession() { }

        public static UserSession Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserSession();
                }
                return _instance;
            }
        }
        public void Login(UserAccount user)
        {
            CurrentUser = user;
        }
        public void Logout()
        {
            CurrentUser = null;
        }
        public bool IsAuthenticated => CurrentUser != null;
        public bool IsAdmin
        {
            get
            {
                return CurrentUser?.RoleId == 1;
            }
        }






        public void AddToCart(Comix comix, int quantity = 1)
        {
            if (CurrentUser == null)
            {
                MessageBox.Show("Пользователь не авторизован!");
                return;
            }

            using (var context = new DataContext())
            {
                // Получаем актуальную информацию о комиксе из базы
                var dbComix = context.Comixes.FirstOrDefault(c => c.Id == comix.Id);

                if (dbComix == null)
                {
                    MessageBox.Show("Комикс не найден.");
                    return;
                }

                // Проверяем количество товара на складе
                if (dbComix.StockQuantity <= 0)
                {
                    MessageBox.Show($"Комикс \"{dbComix.Title}\" закончился на складе.");
                    return;
                }

                if (dbComix.StockQuantity < quantity)
                {
                    MessageBox.Show($"На складе недостаточно товара. Доступно: {dbComix.StockQuantity}");
                    return;
                }

                // Получаем корзину текущего пользователя
                var cart = context.ShoppingCarts
                    .FirstOrDefault(sc => sc.UserId == CurrentUser.Id);

                // Если корзины ещё нет, создаём её
                if (cart == null)
                {
                    cart = new ShoppingCart
                    {
                        UserId = CurrentUser.Id,
                        CreatedAt = DateTime.Now
                    };
                    context.ShoppingCarts.Add(cart);
                    context.SaveChanges();
                }

                // Проверяем, есть ли уже этот товар в корзине
                var cartItem = context.CartItems
                    .FirstOrDefault(ci => ci.CartId == cart.Id && ci.ComixId == dbComix.Id);

                if (cartItem != null)
                {
                    // Проверяем, можем ли мы добавить товар
                    if (cartItem.Quantity + quantity > dbComix.StockQuantity)
                    {
                        MessageBox.Show($"Невозможно добавить больше. Доступно: {dbComix.StockQuantity - cartItem.Quantity}");
                        return;
                    }

                    // Если товар уже есть в корзине, увеличиваем количество
                    cartItem.Quantity += quantity;
                }
                else
                {
                    // Если товара нет, добавляем новый элемент в корзину
                    cartItem = new CartItem
                    {
                        CartId = cart.Id,
                        ComixId = dbComix.Id,
                        Quantity = quantity
                    };
                    context.CartItems.Add(cartItem);
                }

                // Уменьшаем количество товара на складе
                // dbComix.StockQuantity -= quantity;

                context.SaveChanges();
            }

            MessageBox.Show($"Добавлено {quantity} шт. \"{comix.Title}\" в корзину!");
        }
    }
}
