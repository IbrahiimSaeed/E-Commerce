using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared.Dtos.BasketModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class PaymentsController(IServiceManager _serviceManager) : ApiController
    {
        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntentAsync(string basketId)
            => Ok(await _serviceManager.PaymentService.CreateOrUpdatePaymentIntentAsync(basketId));
    }
}
