using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Core;
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
            try
            {
                var employee = await _userService.RegisterEmployeeAsync(model);

                if (employee == null)
                    return BadRequest(new ApiErrorResponse(400, "failed to add employee"));

                return Ok(_mapper.Map<Employee, EmployeeToReturn>(employee));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse(400, ex.Message));
            }
        }

        [HttpPost("register/DeliveryMan")]
        public async Task<ActionResult> RegisterDeliveryMan(DeliveryManRegistrationModel model)
        {
            try
            {
                var deliveryman = await _userService.RegisterDeliveryManAsync(model);
                if (deliveryman == null)
                    return BadRequest(new ApiErrorResponse(400, "failed to add deliveryman"));
                return Ok(_mapper.Map<DeliveryMan, DeliveryManToReturn>(deliveryman));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse(400, ex.Message));
            }
        }

        [HttpPost("register/Merchant")]
        public async Task<ActionResult> RegisterMerchant(MerchantRegistrationModel model)
        {
            try
            {
                var merchant = await _userService.RegisterMerchantAsync(model);
                if (merchant == null)
                    return BadRequest(new ApiErrorResponse(400, "failed to add merchant"));
                return Ok(_mapper.Map<Merchant, MerchantToReturn>(merchant));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse(400, ex.Message));
            }
        }
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginModel model)
        {
            try
            {
                var token = await _userService.LoginAsync(model);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new ApiErrorResponse(401, ex.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiErrorResponse(400, ex.Message));
            }
        }


    }
}
