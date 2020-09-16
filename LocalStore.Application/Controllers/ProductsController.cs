using LocalStore.Application.Models;
using LocalStore.Commons.Models;
using LocalStore.Domain.Models.ProductAggregate;
using LocalStore.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;

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

        [HttpGet("mostsold")]
        public ActionResult<GetMostSoldProductResponse> GetMostSoldProduct()
        {
            var currentTime = DateTime.Now;
            var dateRange = new DateRange
            {
                InitialDate = currentTime.AddDays(-30),
                FinalDate = currentTime
            };

            Product mostSoldProduct =  this._orderService.GetMostSoldProductInDateRange(dateRange);
            decimal revenue = this._orderService.GetRevenueInDateRangeForProductId(dateRange, mostSoldProduct.Id);

            return Ok(new GetMostSoldProductResponse
            {
                Name = mostSoldProduct.Name,
                Revenue = revenue
            });
        }
    }
}
