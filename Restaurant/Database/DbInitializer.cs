using NuGet.Packaging;
using Restaurant.Models;

namespace Restaurant.Database;

public static class DbInitializer
    {
        public static void Initialize(ManagerDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any Cart.
            if (context.CartItems.Any())
            {
                return;   // DB has been seeded
            }

            // Seeding User
            var user = new User();
            context.Users.Add(user);

            // Seeding Categories
            var category1 = new Category { Name = "Category1", Arrangement = 1 };
            var category2 = new Category { Name = "Category2", Arrangement = 2 };
            context.Categories.AddRange(category1, category2);

            // Seeding Menus
            var menu1 = new Menu { Name = "Menu1 C1", Description = "Menu of Category 1", Price = (decimal)1.99, Sold = 0, HasPhoto = false, Arrangement = 0, Category = category1 };
            var menu2 = new Menu { Name = "Menu2 C1", Description = "Another Menu of Category 1", Price = (decimal)2.99, Sold = 1, HasPhoto = false, Arrangement = 1, Category = category1 };
            var menu3 = new Menu { Name = "Menu3 C2", Description = "Menu of Category 2", Price = (decimal)3.99, Sold = 2, HasPhoto = false, Arrangement = 2, Category = category2 };
            var menu4 = new Menu { Name = "Menu4 C0", Description = "Menu with No category", Price = (decimal)0.99, Sold = 3, HasPhoto = false, Arrangement = 3 };
            context.Menus.AddRange(menu1, menu2, menu3, menu4);
            
            // Seeding CartItems
            var cart1 = new CartItem { Menu = menu1, Quantity = 1, User = user };
            var cart2 = new CartItem { Menu = menu3, Quantity = 2, User = user };
            context.CartItems.Add(cart1);
            context.CartItems.Add(cart2);
            
            // Seeding Order
            var order = new Order
            {
                Date = DateTime.UtcNow, Memo = "Without NUTS!", Price = (decimal)12.93, Status = Order.OrderStatusEnum.Paid,
                User = user
            };
            context.Orders.Add(order);
            
            // Seeding OrderItems
            var orderItem1 = new OrderItem { Menu = menu2, Quantity = 3, Order = order };
            var orderItem2 = new OrderItem { Menu = menu4, Quantity = 4, Order = order };
            context.OrderItems.AddRange(orderItem1, orderItem2);

            // Seeding RestaurantConfig
            context.RestaurantConfig.IsOpen = false;
            context.SaveChanges();
        }
    }