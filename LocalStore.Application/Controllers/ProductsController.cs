using LocalStore.Application.Models;
using LocalStore.Commons.Models;
using LocalStore.Domain.Models.ProductAggregate;
using LocalStore.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace LocalStore.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public ProductsController(IOrderService orderService)
        {
            this._orderService = orderService;
        }

        [HttpGet("mostsold/{numberOfProducts}")]
        public ActionResult<IEnumerable<GetMostSoldProductResponse>> GetNMostSoldProducts(int numberOfProducts, [FromQuery] DateRange dateRange)
        {
            IEnumerable<Product> mostSoldProducts = this._orderService.GetTopNMostSoldProductsInDateRange(dateRange, numberOfProducts);

            return Ok(mostSoldProducts.Select(product => new GetMostSoldProductResponse
            {
                Name = product.Name,
                Revenue = this._orderService.GetRevenueInDateRangeForProductId(dateRange, product.Id)
            }));
        }

        [HttpGet("lesssold/{numberOfProducts}")]
        public ActionResult<IEnumerable<GetMostSoldProductResponse>> GetNLessSoldProducts(int numberOfProducts, [FromQuery] DateRange dateRange)
        {
            IEnumerable<Product> mostSoldProducts = this._orderService.GetTopNLessSoldProductsInDateRange(dateRange, numberOfProducts);

            return Ok(mostSoldProducts.Select(product => new GetMostSoldProductResponse
            {
                Name = product.Name,
                Revenue = this._orderService.GetRevenueInDateRangeForProductId(dateRange, product.Id)
            }));
        }
    }
}
