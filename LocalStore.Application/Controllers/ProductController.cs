using LocalStore.Domain.Models.ProductAggregate;
using LocalStore.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LocalStore.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public ProductController(IOrderService orderService)
        {
            this._orderService = orderService;
        }

        [HttpGet("mostsold")]
        public Product GetMostSoldProduct()
        {
            DateTime finalDate = DateTime.Now;
            DateTime initialDate = finalDate.AddDays(-30);

            return this._orderService.GetMostSoldProductInDateRange(initialDate, finalDate);
        }
    }
}
