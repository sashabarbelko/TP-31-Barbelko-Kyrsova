using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DBContext _dbContext;
        public OrderRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateOrder(Order order)
        {
            order.OrderID = _dbContext.GenerateOrderID();
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
        }
        public Order GetOrderById(int OrderID)
        {
            return _dbContext.Orders.FirstOrDefault(o => o.OrderID == OrderID);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _dbContext.Orders.ToList();
        }
    }
}
