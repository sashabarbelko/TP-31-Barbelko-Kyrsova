using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class DBContext
    {
        public List<User> Users { get; set; } = new List<User>();
        public List<Product> Products { get; set; } = new List<Product>();
        public List<Order> Orders { get; set; } = new List<Order>();

        private static int orderIdCounter = 1;
        private static int userIdCounter = 3;

        public DBContext()
        {
            Products.Add(new Product { ProductID = 1, ProductName = "Laptop", Price = 1000, Stock = 10 });
            Products.Add(new Product { ProductID = 2, ProductName = "Phone", Price = 500, Stock = 20 });
            Products.Add(new Product { ProductID = 3, ProductName = "Headphones", Price = 100, Stock = 30 });

            Users.Add(new User { Username = "Admin", Password = "admin", Balance = 100000, UserId = 1 });
            Users.Add(new User { Username = "Sasha", Password = "cfif", Balance = 1000, UserId = 2 });
        }

        public void SaveChanges()
        {
            Console.WriteLine("Changes have been saved to the database.");
        }

        public int GenerateOrderID()
        {
            return orderIdCounter++;
        }
        public int GenerateUserID()
        {
            return userIdCounter++;
        }
    }

}
