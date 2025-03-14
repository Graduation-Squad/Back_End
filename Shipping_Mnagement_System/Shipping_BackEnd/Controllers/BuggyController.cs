using Microsoft.AspNetCore.Mvc;
using Shipping.Repository.Data;
using Shipping_APIs.Errors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shipping_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly ShippingContext _shippingContext;

        public BuggyController(ShippingContext shippingContext)
        {
            _shippingContext = shippingContext;
        }

        [HttpGet("NotFound")]
        public ActionResult GetNotFoundResponse()
        {
            var product = _shippingContext.Employees.Find(100);
            if (product is null)
                return NotFound(new ApiErrorResponse(404));

            return Ok(product);
        }

        [HttpGet("BadRequest")]
        public ActionResult GetBadRequestResponse()
        {
            return BadRequest(new ApiErrorResponse(400));
        }

        [HttpGet("ServerError")]
        public ActionResult GetServerErrorResponse()
        {
            var product = _shippingContext.Employees.Find(1000);
            if (product == null)
                return StatusCode(500, new ApiErrorResponse(500, "Internal Server Error"));

            var productToReturn = product.ToString();
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
