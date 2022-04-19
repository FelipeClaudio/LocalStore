using LocalStore.Application.Models;
using LocalStore.Commons.Models;
using LocalStore.Domain.Models.Order;
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
            IEnumerable<Order> ordersInDateRange = this._orderService.GetAllOrdersInDateRange(dateRange);
            var orderInfos = ordersInDateRange.Select(order => new OrderInfo
            {
                OrderDate = order.OrderDate,
                OrderId = order.Id,
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
            IEnumerable<OrderAdditionalInfo> orderAdditionalInfos = this._orderService.GetRevenuesInDateRangeForProductId(dateRange);

            var sellingReport = mostSoldProducts.Select(product => 
            {
                var currentOrderAdditionalInfo = orderAdditionalInfos.FirstOrDefault(o => o.ProductId == product.Id);
                return new GetProductSellingReport
                {
                    Name = product.Name,
                    Revenue = currentOrderAdditionalInfo.Revenue,
                    Quantity = currentOrderAdditionalInfo.Quantity
                };
            });

            return Ok(sellingReport);
        }

        [HttpGet("lesssold/{numberOfProducts}")]
        public ActionResult<IEnumerable<GetProductSellingReport>> GetNLessSoldProducts(int numberOfProducts, [FromQuery] DateRange dateRange)
        {
            IEnumerable<Product> lessSoldProducts = this._orderService.GetTopNLessSoldProductsInDateRange(dateRange, numberOfProducts);
            IEnumerable<OrderAdditionalInfo> orderAdditionalInfos = this._orderService.GetRevenuesInDateRangeForProductId(dateRange);

            var sellingReport = lessSoldProducts.Select(product =>
            {
                var currentOrderAdditionalInfo = orderAdditionalInfos.FirstOrDefault(o => o.ProductId == product.Id);
                return new GetProductSellingReport
                {
                    Name = product.Name,
                    Revenue = currentOrderAdditionalInfo.Revenue,
                    Quantity = currentOrderAdditionalInfo.Quantity
                };
            });

            return Ok(sellingReport);
        }
    }
}
