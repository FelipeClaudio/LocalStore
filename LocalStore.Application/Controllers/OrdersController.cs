using LocalStore.Application.Models;
using LocalStore.Commons.Models;
using LocalStore.Domain.Models.OrderAggregate;
using LocalStore.Domain.Models.ProductAggregate;
using LocalStore.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace LocalStore.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public OrdersController(IOrderService orderService, IProductService productService)
        {
            this._orderService = orderService;
            this._productService = productService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderInfo>> GetAllOrdersInDateRange([FromQuery] DateRange dateRange)
        {
            IEnumerable<Order> ordersInDateRange = this._orderService.GetAllOrdersForDateRange(dateRange);
            var orderInfos = ordersInDateRange.Select(order => new OrderInfo
            {
                OrderDate = order.OrderDate,
                OrderItemInfos = order.Items.Select(orderItem => new OrderItemInfo
                {
                    OrderItemId = orderItem.Id,
                    Product = this._productService.GetProductById(orderItem.ProductId),
                    Quantity = orderItem.Quantity,
                    UnityPrice = orderItem.UnitPrice
                })
            });

            return Ok(orderInfos);
        }

        [HttpGet("mostsold/{numberOfProducts}")]
        public ActionResult<IEnumerable<GetProductSellingReport>> GetNMostSoldProducts(int numberOfProducts, [FromQuery] DateRange dateRange)
        {
            IEnumerable<Product> mostSoldProducts = this._orderService.GetTopNMostSoldProductsInDateRange(dateRange, numberOfProducts);

            return Ok(mostSoldProducts.Select(product => new GetProductSellingReport
            {
                Name = product.Name,
                Revenue = this._orderService.GetRevenueInDateRangeForProductId(dateRange, product.Id)
            }));
        }

        [HttpGet("lesssold/{numberOfProducts}")]
        public ActionResult<IEnumerable<GetProductSellingReport>> GetNLessSoldProducts(int numberOfProducts, [FromQuery] DateRange dateRange)
        {
            IEnumerable<Product> mostSoldProducts = this._orderService.GetTopNLessSoldProductsInDateRange(dateRange, numberOfProducts);

            return Ok(mostSoldProducts.Select(product => new GetProductSellingReport
            {
                Name = product.Name,
                Revenue = this._orderService.GetRevenueInDateRangeForProductId(dateRange, product.Id)
            }));
        }
    }
}
