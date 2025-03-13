using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Shipping_APIs.Errors;

namespace Shipping_APIs.Controllers
{
    [Route("errors/{statusCode}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]

    public class ErrorsController : ControllerBase
    {
        public IActionResult Error(int statusCode)
        {
            return NotFound(new ApiErrorResponse(statusCode, "Not Found EndPoint"));
        }
    }
}
