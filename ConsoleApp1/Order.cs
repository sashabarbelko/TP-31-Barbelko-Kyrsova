using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Order
    {
        public int OrderID { get; set; }
        public User Buyer { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public decimal Price { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
