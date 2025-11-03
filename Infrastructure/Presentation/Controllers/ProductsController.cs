using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResultDto>>> GetAllProductsAsync()
            => Ok(await _serviceManager.ProductService.GetAllProductsAsync());

        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrandsAsync()
            => Ok(await _serviceManager.ProductService.GetAllBrandsAsync());

        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypesAsync()
            => Ok(await _serviceManager.ProductService.GetAllTypesAsync());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductResultDto>> GetProductByIdAsync(int id)
            => Ok(await _serviceManager.ProductService.GetProductByIdAsync(id));
    }
}
