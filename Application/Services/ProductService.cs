using Application.ViewModels;
using Data;
using Data.EF;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            List<ProductForList> productsList = new List<ProductForList>();
            List<Product> products = db.Products.GetAll().ToList();
            foreach (Product product in products)
            {
                ProductForList productForList = new ProductForList();
                productForList.ProductId = product.Id;
                productForList.Price = product.Price;
                productForList.Name = product.Name;
                productForList.PhotoPath = product.PhotoPath;
                productsList.Add(productForList);
            }
            return productsList; 
        }
    }
}
