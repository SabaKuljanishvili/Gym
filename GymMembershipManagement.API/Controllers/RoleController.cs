using GymMembershipManagement.SERVICE.DTOs.Role;
using GymMembershipManagement.SERVICE.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymMembershipManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("GetAllRoles")]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetAllRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }

        [HttpGet("GetRoleById/{id:int}")]
        public async Task<ActionResult<RoleDto>> GetRoleById(int id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null) return NotFound("Role not found.");
            return Ok(role);
        }

        [HttpPost("CreateRole")]
        public async Task<ActionResult<RoleDto>> CreateRole([FromBody] CreateRoleDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var role = await _roleService.CreateRoleAsync(model);
            return CreatedAtAction(nameof(GetRoleById), new { id = role.RoleId }, role);
        }

        [HttpPut("UpdateRole/{id:int}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] UpdateRoleDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _roleService.UpdateRoleAsync(id, model);
            if (!result) return NotFound("Role not found.");
            return NoContent();
        }

        // FIX: Was "UpdateRoleDto/{id}" — wrong route, now "DeleteRole/{id}"
        [HttpDelete("DeleteRole/{id:int}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var result = await _roleService.DeleteRoleAsync(id);
            if (!result) return NotFound("Role not found.");
            return NoContent();
        }
    }
}
