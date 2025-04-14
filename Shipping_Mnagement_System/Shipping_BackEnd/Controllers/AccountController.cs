using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shipping.Core;
using Shipping.Core.DomainModels.Identity;
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
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserService userService, IMapper mapper, UserManager<AppUser> userManager)
        {
            _userService = userService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost("register")]
        [Authorize(Roles = "Admin")]  
        public async Task<ActionResult> Register(RegisterRequest model)
        {
            try
            {
                var user = await _userService.RegisterAsync(model);
                return Ok(user);
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
                var mytoken = await _userService.LoginAsync(model);
                var user = await _userManager.FindByEmailAsync(model.Email);
                var roles = await _userManager.GetRolesAsync(user);

                return Ok(new
                {
                    //Token = token,
                    User = new
                    {
                        //Id = user.Id,
                        //Email = user.Email,
                        //UserName = user.UserName,
                        //FullName = user.FullName,
                        //RoleId = roles.FirstOrDefault() // or whatever identifies the role
                        userID = user.Id,
                        name = user.FullName,
                        email = user.Email,
                        role = roles.FirstOrDefault(),
                        roleId = 1,
                        token = mytoken,
                        userName = user.UserName,
                        status = user.IsActive

                    }
                });
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
