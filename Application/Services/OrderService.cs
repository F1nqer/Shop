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
            var orderModel = db.Orders.GetActive();
            var order = new OrderActive();
            order.Address = orderModel.Address;
            order.OrderId = orderModel.Id;
            order.CardNumber = orderModel.CardNumber;
            order.State = orderModel.State.Name;
            var productsForOrder = orderModel.Products.Select(p => new ProductForOrder
            {
                Name = p.Name,
                ProductId = p.Id,
                PriceSum = orderModel.Products.Where(pr => pr.Id == p.Id).Sum(pr => p.Price),
                Count = orderModel.Products.Where(pr => pr.Id == p.Id).Count()
            }).Distinct().ToList();
            order.Products = productsForOrder;
            
            return order; 
        }

        public List<OrderForList> GetOrders()
        {
            var orders = db.Orders.GetAll();
            var getOrders = orders
                        .Select(o => new OrderForList 
                                    { Address = o.Address, 
                                      OrderId = o.Id, 
                                      State = o.State.Name })
                        .ToList();
            return getOrders;
        }

        public OrderFull GetOrderById(int OrderId)
        {
            var orderFull = new OrderFull();
            var order = db.Orders.GetById(OrderId);
            var orderProductsHistory = db.Orders.GetOrderProductsHistory(OrderId).ToList();
            var orderHistories = db.Orders.GetOrdersHistory(OrderId).ToList();
            orderFull.OrderId = order.Id;
            orderFull.Address = order.Address;
            orderFull.State = order.State.Name;
            orderFull.CardNumber = order.CardNumber;
            orderFull.Products = order.Products
                                        .Select(p => new ProductForOrder
                                        {
                                            Name = p.Name,
                                            ProductId = p.Id,
                                            PriceSum = order.Products.Where(pr => pr.Id == p.Id).Sum(pr => p.Price),
                                            Count = order.Products.Where(pr => pr.Id == p.Id).Count()
                                        }).Distinct().ToList();
            orderFull.OrdersHistory = orderHistories
                                        .Select(oh => new OrderHistoryView
                                        {
                                            Address = oh.Address,
                                            OrderId = oh.OrderId,
                                            CardNumber = oh.CardNumber,
                                            ChangeAt = oh.PeriodStart,
                                            State = oh.State.Name
                                        }).ToList();
            orderFull.ProductsHistory = orderProductsHistory
                                        .Select(oph => new ProductHistory
                                        {
                                            ProductId = oph.ProductId,
                                            Action = oph.Action,
                                            Name = oph.Product.Name
                                        })
                                        .ToList();
            return orderFull;

            
        }
        public void BuyOrder(OrderActive orderActive)
        {
            var order = db.Orders.GetById(orderActive.OrderId);
            order.CardNumber = orderActive.CardNumber;
            order.Address = orderActive.Address;
            order.State = db.Orders.GetOrderStates().Where(s => s.Code == "Buyed").First();
            db.Save();
        }

        public void DoneOrder(OrderActive orderActive)
        {
            var order = db.Orders.GetById(orderActive.OrderId);
            order.CardNumber = orderActive.CardNumber;
            order.Address = orderActive.Address;
            order.State = db.Orders.GetOrderStates().Where(s => s.Code == "Done").First();
            db.Save();
        }
        public void AddProductToOrder(int productId)
        {
            var order = db.Orders.GetActive();

            db.Orders.AddProduct(order, productId);
            db.Save();
        }
        public void RemoveProductFromOrder(int productId)
        {
            var order = db.Orders.GetActive();

            db.Orders.RemoveProduct(order, productId);
            db.Save();
        }
    }
}
