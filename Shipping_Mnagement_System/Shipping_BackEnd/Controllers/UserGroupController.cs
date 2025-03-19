using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Core.Services.Contracts;

namespace Shipping_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGroupController : ControllerBase
    {
        private readonly IUserGroupService _userGroupService;

        public UserGroupController(IUserGroupService userGroupService)
        {
            _userGroupService = userGroupService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserGroup([FromBody] string name)
        {
            var userGroup = await _userGroupService.CreateUserGroupAsync(name);
            return Ok(userGroup);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserGroups()
        {
            var userGroups = await _userGroupService.GetAllUserGroupsAsync();
            return Ok(userGroups);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserGroupById(int id)
        {
            var userGroup = await _userGroupService.GetUserGroupByIdAsync(id);
            return Ok(userGroup);
        }

        [HttpPost("{userGroupId}/permissions/{permissionId}")]
        public async Task<IActionResult> AddPermissionToUserGroup(int userGroupId, int permissionId)
        {
            await _userGroupService.AddPermissionToUserGroupAsync(userGroupId, permissionId);
            return NoContent();
        }

        [HttpPost("{userGroupId}/permissions")]
        public async Task<IActionResult> AddPermissionsToUserGroup(int userGroupId, [FromBody] IEnumerable<int> permissionIds)
        {
            await _userGroupService.AddPermissionsToUserGroupAsync(userGroupId, permissionIds);
            return NoContent();
        }

        [HttpDelete("{userGroupId}/permissions/{permissionId}")]
        public async Task<IActionResult> RemovePermissionFromUserGroup(int userGroupId, int permissionId)
        {
            await _userGroupService.RemovePermissionFromUserGroupAsync(userGroupId, permissionId);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserGroup(int id)
        {
            await _userGroupService.DeleteUserGroupAsync(id);
            return NoContent();
        }
    }
}
