using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shipping.Core.DomainModels;
using Shipping.Core.Services.Contracts;

namespace Shipping_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        //List all branches
        [HttpGet]
        public async Task<IActionResult> GetAllBranches()
        {
            var branches = await _branchService.GetAllAsync();
            return Ok(branches);
        }

        //Get branch details
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBranchById(int id)
        {
            var branch = await _branchService.GetByIdAsync(id);
            if (branch == null) return NotFound();
            return Ok(branch);
        }

        //Create a new branch
        [HttpPost]
        public async Task<IActionResult> CreateBranch([FromBody] Branch branch)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _branchService.AddAsync(branch);
            return CreatedAtAction(nameof(GetBranchById), new { id = branch.Id }, branch);
        }

        //Update a branch
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBranch(int id, [FromBody] Branch branch)
        {
            if (id != branch.Id) return BadRequest("ID mismatch");

            await _branchService.UpdateAsync(branch);
            return NoContent();
        }

        //Delete a branch
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            await _branchService.DeleteAsync(id);
            return NoContent();
        }

        //Activate/Deactivate a branch
        [HttpPut("{id}/status")]
        public async Task<IActionResult> ToggleBranchStatus(int id)
        {
            var branch = await _branchService.GetByIdAsync(id);
            if (branch == null) return NotFound("Branch not found");

            branch.IsActive = !branch.IsActive;
            await _branchService.UpdateAsync(branch);
            return Ok(new { message = "Branch status updated", IsActive = branch.IsActive });
        }

        //Assign users to a branch
        [HttpPost("{id}/users")]
        public async Task<IActionResult> AssignUsersToBranch(int id, [FromBody] List<int> userIds)
        {
            var branch = await _branchService.GetByIdAsync(id);
            if (branch == null) return NotFound("Branch not found");

            await _branchService.AssignUsersToBranch(id, userIds);
            return Ok(new { message = "Users assigned to branch successfully" });
        }

        //Remove a user from a branch
        [HttpDelete("{id}/users/{userId}")]
        public async Task<IActionResult> RemoveUserFromBranch(int id, int userId)
        {
            var branch = await _branchService.GetByIdAsync(id);
            if (branch == null) return NotFound("Branch not found");

            await _branchService.RemoveUserFromBranch(id, userId);
            return Ok(new { message = "User removed from branch successfully" });
        }
    }
}
