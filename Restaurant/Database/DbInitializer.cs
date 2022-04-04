using Restaurant.Models;

namespace Restaurant.Database;

public static class DbInitializer
    {
        public static void Initialize(ManagerDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any Cart.
            if (context.Carts.Any())
            {
                return;   // DB has been seeded
            }

            var Menus = new Menu[]
            {
                new Menu
                {
                    Name = "Menu1", Description = "Desc1", Price = (decimal)9.99, Sold = 0, HasPhoto = false,
                    Arrangement = 0
                },
                new Menu
                {
                    Name = "Menu2", Description = "Desc2", Price = (decimal)5.99, Sold = 1, HasPhoto = false,
                    Arrangement = 1
                },
            };
            foreach (Menu s in Menus)
            {
                context.Menus.Add(s);
            }
            context.SaveChanges();
            
            var Carts = new Cart[]
            {
                new Cart
                {
                    Menu = Menus[0], Quantity = 2
                },
                new Cart
                {
                    Menu = Menus[1], Quantity = 4
                }
            };
            foreach (Cart s in Carts)
            {
                context.Carts.Add(s);
            }
            context.SaveChanges();
        }
    }