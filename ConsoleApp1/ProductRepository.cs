using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class ProductRepository : IProductRepository
    {
        private readonly DBContext _dbContext;
        public ProductRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateProduct(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        public void DeleteProduct(int ProductID)
        {
            var product = _dbContext.Products.FirstOrDefault(u => u.ProductID == ProductID);
            if (product != null)
            {
                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();
            }
        }
        public Product GetProductById(int ProductID)
        {
            return _dbContext.Products.FirstOrDefault(p => p.ProductID == ProductID);
        }

        public void UpdateProduct(Product product)  
        {
            var existingProduct = _dbContext.Products.FirstOrDefault(p => p.ProductID == product.ProductID);
            if (existingProduct != null)
            {
                existingProduct.ProductName = product.ProductName;
                existingProduct.Price = product.Price;
                existingProduct.Stock = product.Stock;
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _dbContext.Products.ToList();
        }
    }

}
