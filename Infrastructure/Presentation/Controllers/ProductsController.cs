using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared;
using Shared.Dtos;
using Shared.Enums;
using Shared.ErrorModels;
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
        public async Task<ActionResult<PaginatedResult<ProductResultDto>>> GetAllProductsAsync([FromQuery]ProductSpecificationParameters parameters)
            => Ok(await _serviceManager.ProductService.GetAllProductsAsync(parameters));

        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrandsAsync()
            => Ok(await _serviceManager.ProductService.GetAllBrandsAsync());

        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypesAsync()
            => Ok(await _serviceManager.ProductService.GetAllTypesAsync());


        [ProducesResponseType(typeof(ProductResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ErrorDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationErrorResponse), StatusCodes.Status400BadRequest)]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductResultDto>> GetProductByIdAsync(int id)
            => Ok(await _serviceManager.ProductService.GetProductByIdAsync(id));
    }
}
