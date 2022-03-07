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
    public class OrderRepository: IRepository <Order>
    {
        private OrderContext db;

        public OrderRepository(OrderContext context)
        {
            this.db = context;
        }
        //Get Actions
        public IQueryable<Order> GetAll()
        {
            return db.Orders.Include(o => o.State);
        }
        public Order GetById(int id)
        {
            return db.Orders.Include(o => o.State).Include(o => o.Products).Where(o => o.Id == id).First();
        }
        public Order GetActive()
        {
            Order order;
            try
            {
                order = db.Orders.Include(o => o.Products).Include(o => o.State).Where(o => o.State.Code == "Active").First();
                return order;
            }
            catch 
            { 
                db.Orders.Add(new Order { State = db.State.Find(1), Address = null, CardNumber = 0, StateId = 1 });
                db.SaveChanges();
                return db.Orders.Include(o => o.Products).Include(o => o.State).Where(o => o.State.Code == "Active").First();
            }
        }
        public IQueryable<OrderHistory> GetOrdersHistory(int orderId)
        {
            return db.OrdersHistory
                        .Include(o => o.State)
                        .Where(o => o.OrderId == orderId)
                        .OrderByDescending(o => o.PeriodStart);
        }

        public IQueryable<OrderProductsHistory> GetOrderProductsHistory(int orderId)
        {
            return db.OrderProductsHistory
                    .Include(o => o.Order)
                    .Include(o => o.Product)
                    .Where(o => o.OrderId == orderId)
                    .OrderByDescending(o => o.PeriodStart);
        }

        public IQueryable<State> GetOrderStates()
        {
            return db.State;
        }

        //Add and create actions
        public void Create(Order order)
        {
            db.Orders.Add(order);
        }
        
        public void AddProduct(Order order, int productId)
        {
            order.Products.Remove(db.Products.Find(productId));
            db.Update(order);
        }

        //Update and change actins
        public void Update(Order order)
        {
            db.Update(order);
        }
        public void ChangeState(Order order, State state)
        {
            order.State = state;
            db.Update(order);
        }

        //Delete actions
        public void Delete(int id)
        {
            Order order = db.Orders.Find(id);
            if (order != null)
                db.Orders.Remove(order);
        }
        public void RemoveProduct(Order order, int productId) 
        {
            order.Products.Remove(db.Products.Find(productId));
            db.Update(order);
        }
    }
}
