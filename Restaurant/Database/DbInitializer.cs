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
            var cart1 = new CartItem
            {
                Menu = Menus[0], Quantity = 2
            };
            
            var carts = new CartItem[]
            {
                cart1,
                new CartItem
                {
                    Menu = Menus[1], Quantity = 4
                }
            };
            foreach (CartItem s in carts)
            {
                context.Carts.Add(s);
            }
            context.SaveChanges();

            context.Users.Add(new User()
            {
                Auth0Id = Guid.Parse("55543fb9-7554-4213-80c5-4edfacecdd72"),
                Carts = new List<CartItem> { cart1 }
            });

            var categories = new Category[]
            {
                new Category
                {
                    Name = "Category1"
                },
                new Category
                {
                    Name = "Category2"
                }
            };
            foreach (Category s in categories)
            {
                context.Categories.Add(s);
            }
            context.SaveChanges();
        }
    }