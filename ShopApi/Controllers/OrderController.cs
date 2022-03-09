using Application.Services;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        private OrderService _orderService;
        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet()]
        public OrderActive GetActiveOrder()
        {
            return _orderService.GetActiveOrder();
        }

        [HttpGet]
        public List<OrderForList> GetAllOrders()
        {
            return _orderService.GetOrders();
        }

        [HttpGet("{orderId}")]
        public OrderFull GetFullOrderById(int orderId)
        {
            return _orderService.GetOrderById(orderId);
        }

        [HttpPost]
        public async Task<ActionResult> AddProductToOrder(int productId)
        {
            if (productId < 1)
            {
                return BadRequest();
            }

            _orderService.AddProductToOrder(productId);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveProductFromOrder(int productId)
        {
            if (productId < 1)
            {
                return BadRequest();
            }

            _orderService.RemoveProductFromOrder(productId);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> BuyOrder(OrderActive orderActive)
        {
            if (orderActive == null)
            {
                return BadRequest();
            }
            _orderService.BuyOrder(orderActive);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> DoneOrder(OrderActive orderActive)
        {
            if (orderActive == null)
            {
                return BadRequest();
            }
            _orderService.DoneOrder(orderActive);
            return Ok();
        }
    }
}
