﻿using EcommerceApp.Server.Services.PaymentService;
using EcommerceApp.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost("checkout"),Authorize] 
        public async Task<ActionResult<string>> CreateCheckoutSessionAsync(){
            var session = await _paymentService.CreateCheckoutSessionAsync();
            return Ok(session.Url);
        }

        /**
         * This is a webhook endpoint that Stripe will call when the payment is complete.
         */
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<bool>>> FulfillOrderAsync()
        {
            var response = await _paymentService.FulfillOrderAsync(Request);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }
    }
}
