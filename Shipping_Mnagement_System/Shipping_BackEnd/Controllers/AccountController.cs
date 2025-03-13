using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Core.Models;
using Shipping.Models;
using Shipping.Service;
using Shipping_APIs.Errors;

namespace Shipping_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public AccountController(UserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("register/Employee")]
        public async Task<ActionResult> RegisterEmployee(EmployeeRegistrationModel model)
        {
            var employee = await _userService.RegisterEmployeeAsync(model);

            if(employee == null)
                return BadRequest(new ApiErrorResponse(400, "failed to add employee"));

            return Ok(_mapper.Map<Employee, EmployeeToReturn>(employee));
        }
    }
}
