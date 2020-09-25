using LocalStore.Application.Models;
using LocalStore.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LocalStore.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            this._productService = productService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderInfo>> GetAllProducts()
        {
            return Ok(this._productService.GetAllProducts());
        }
    }
}
