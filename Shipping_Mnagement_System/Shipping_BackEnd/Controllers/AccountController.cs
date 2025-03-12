using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Models;
using Shipping.Service;

namespace Shipping_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserService _userService;

        public AccountController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register/Employee")]
        public async Task<ActionResult> RegisterEmployee(EmployeeRegistrationModel model)
        {
            var result = await _userService.RegisterEmployeeAsync(model);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(result);
        }
    }
}
