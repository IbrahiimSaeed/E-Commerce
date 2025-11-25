using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared.Dtos.BasketModule;
using Shared.Dtos.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Authorize]
    public class BasketController(IServiceManager _serviceManager) : ApiController
    {
        [HttpGet]
        [ProducesResponseType(typeof(BasketDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<BasketDto>> GetBasketAsync(string id)
            => Ok(await _serviceManager.BasketService.GetBasketAsync(id));

        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basketDto)
            => Ok(await _serviceManager.BasketService.CreateOrUpdateBasketAsync(basketDto));

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBasket(string id)
        {
            await _serviceManager.BasketService.DeleteBasketAsync(id);
            return NoContent(); //204
        }
    }
}
