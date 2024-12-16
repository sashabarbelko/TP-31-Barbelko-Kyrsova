using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
        Order GetOrderById(int OrderID);
        IEnumerable<Order> GetAllOrders();
    }
}
