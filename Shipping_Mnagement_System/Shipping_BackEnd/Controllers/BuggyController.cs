﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shipping.Repository.Data;
using Shipping_APIs.Errors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shipping_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class BuggyController : ControllerBase
    {
        private readonly ShippingContext _shippingContext;

        public BuggyController(ShippingContext shippingContext)
        {
            _shippingContext = shippingContext;
        }

        [HttpGet("NotFound")]
        [Authorize(Roles = "Employee")]
        public ActionResult GetNotFoundResponse()
        {
            if (true)
                return NotFound(new ApiErrorResponse(404));
        }

        [HttpGet("BadRequest")]
        public ActionResult GetBadRequestResponse()
        {
            return BadRequest(new ApiErrorResponse(400));
        }

        [HttpGet("ServerError")]
        public ActionResult GetServerErrorResponse()
        {
            var userGroup = _shippingContext.UserGroups.Find(1000);
            if (userGroup == null)
                return StatusCode(500, new ApiErrorResponse(500, "Internal Server Error"));

            var productToReturn = userGroup.ToString();
            return Ok(productToReturn);
        }


        [HttpGet("ValidationError/{id}")]
        public ActionResult GetValidationErrorResponse(int id)
        {
            return Ok();
        }

        [HttpGet("Unauthorized")]
        public ActionResult GetUnauthorizedResponse()
        {
            return Unauthorized(new ApiErrorResponse(401));
        }
    }
}
