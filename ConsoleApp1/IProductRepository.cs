using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public interface IProductRepository
    {
        void CreateProduct(Product product);
        void DeleteProduct(int ProductId);
        void UpdateProduct(Product product);
        Product GetProductById(int ProductID);
        IEnumerable<Product> GetAllProducts();
    }
}
