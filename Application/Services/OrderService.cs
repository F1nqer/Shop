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
    public class OrderService
    {
        UoW db;
        public OrderService(OrderContext dbContext)
        {
            db = new UoW(dbContext);
        }
        public OrderActive GetActiveOrder()
        {
            Order orderModel = db.Orders.GetActive();
            OrderActive order = new OrderActive();
            order.Address = orderModel.Address;
            order.OrderId = orderModel.Id;
            order.CardNumber = orderModel.CardNumber;
            order.State = orderModel.State.Name;
            foreach(Product p in orderModel.Products)
            {
                ProductForOrder product = new ProductForOrder();
                if(!order.Products.Exists(o => o.ProductId == p.Id))
                {
                    product.ProductId = p.Id;
                    product.Name = p.Name;
                    product.PriceSum = orderModel.Products.Where(pr => pr.Id == p.Id).Sum(pr => p.Price);
                    product.Count = orderModel.Products.Where(pr => pr.Id == p.Id).Count();
                    order.Products.Add(product);
                }   
            }
            return order; 
        }

        public List<OrderForList> GetOrders()
        {
            List<OrderForList> getOrders = new List<OrderForList>();
            List<Order> orders = db.Orders.GetAll().ToList();
            foreach (Order order in orders)
            {
                OrderForList getOrder = new OrderForList();
                getOrder.OrderId = order.Id;
                getOrder.Address = order.Address;
                getOrder.State = order.State.Name;
                getOrders.Add(getOrder);
            }
            return getOrders;
        }

        public OrderFull GetOrderById(int OrderId)
        {
            OrderFull orderFull = new OrderFull();

            Order order = db.Orders.GetById(OrderId);
            List<OrderProductsHistory> orderProductsHistory = db.Orders.GetOrderProductsHistory(OrderId).ToList();
            List<OrderHistory> orderHistories = db.Orders.GetOrdersHistory(OrderId).ToList();
            List<ProductForOrder> products = new List<ProductForOrder>();
            List<OrderHistoryView> orderHistoryViews = new List<OrderHistoryView>();
            List<ProductHistory> productHistories = new List<ProductHistory>();
            orderFull.OrderId = order.Id;
            orderFull.Address = order.Address;
            orderFull.State = order.State.Name;
            orderFull.CardNumber = order.CardNumber;
            foreach(Product p in order.Products)
            {
                ProductForOrder product = new ProductForOrder();
                if (!orderFull.Products.Exists(o => o.ProductId == p.Id))
                {
                    product.ProductId = p.Id;
                    product.Name = p.Name;
                    product.PriceSum = order.Products.Where(pr => pr.Id == p.Id).Sum(pr => p.Price);
                    product.Count = order.Products.Where(pr => pr.Id == p.Id).Count();
                    orderFull.Products.Add(product);
                }
            }
            foreach(OrderHistory oh in orderHistories)
            {
                try
                {
                    OrderHistoryView ohv = new OrderHistoryView();
                    ohv.Address = oh.Address;
                    ohv.OrderId = oh.OrderId;
                    ohv.CardNumber = oh.CardNumber;
                    ohv.ChangeAt = oh.PeriodStart;
                    ohv.State = oh.State.Name;
                    orderFull.OrdersHistory.Add(ohv);
                }
                catch
                {
                    break;
                }
            }
            foreach(OrderProductsHistory oph in orderProductsHistory) 
            {
                try
                {
                    ProductHistory product = new ProductHistory();
                    product.ProductId = oph.ProductId;
                    product.Action = oph.Action;
                    product.Name = oph.Product.Name;
                    orderFull.ProductsHistory.Add(product);
                }
                catch
                {
                    break;
                }
            }
            return orderFull;

            
        }
        public void BuyOrder()
        {
            Order order = db.Orders.GetActive();
            order.State = db.Orders.GetOrderStates().Where(s => s.Code == "Active").First();
            db.Orders.Update(order);
            db.Save();
        }
        public void AddProductToOrder(int productId)
        {
            Order order = db.Orders.GetActive();

            db.Orders.AddProduct(order, productId);
            db.Save();
        }
        public void RemoveProductFromOrder(int productId)
        {
            Order order = db.Orders.GetActive();

            db.Orders.RemoveProduct(order, productId);
            db.Save();
        }
    }
}
