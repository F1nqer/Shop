using Application.ViewModels;
using Data;
using Data.EF;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService
    {
        UoW db;
        public ProductService(OrderContext dbContext)
        {
            db = new UoW(dbContext);
        }

        public List<ProductForList> GetProducts()
        {
            var products = db.Products.GetAll();
            var productsList = products
                            .Select(p => new ProductForList
                            {
                                ProductId = p.Id,
                                Price = p.Price,
                                Name = p.Name,
                                PhotoPath = p.PhotoPath
                            }).ToList();
            return productsList;
        }

        public async Task<List<ProductForList>> GetProductsAsync()
        {
            var products = await db.Products.GetAllAsync();
            var productsList = products
                            .Select(p => new ProductForList
                            {
                                ProductId = p.Id,
                                Price = p.Price,
                                Name = p.Name,
                                PhotoPath = p.PhotoPath
                            }).ToList();
            return productsList;
        }
    }
}
