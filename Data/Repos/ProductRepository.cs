using Data.EF;
using Domain;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repos
{
    public class ProductRepository: IRepository <Product>
    {
        private OrderContext db;

        public ProductRepository(OrderContext context)
        {
            this.db = context;
        }

        public IQueryable<Product> GetAll()
        {
            return db.Products;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await db.Products.ToListAsync();
        }

        public Product GetById(int id)
        {
            return db.Products.Find(id);
        }

        public void Create(Product Product)
        {
            db.Products.Add(Product);
        }

        public void Update(Product Product)
        {
            db.Update(Product);
        }



        public void Delete(int id)
        {
            Product Product = db.Products.Find(id);
            if (Product != null)
                db.Products.Remove(Product);
        }
    }
}
