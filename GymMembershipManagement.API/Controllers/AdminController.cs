using GymMembershipManagement.SERVICE.DTOs.User;
using GymMembershipManagement.SERVICE.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymMembershipManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        // Admin only
        [HttpPost("AddUser")]
        public async Task<ActionResult<UserDTO>> AddUser([FromBody] UserRegisterModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = await _adminService.AddUser(model);
            return Ok(user);
        }

        // Admin only
        [HttpGet("GetUserById/{userId:int}")]
        public async Task<ActionResult<UserDTO>> GetUserById(int userId)
        {
            var user = await _adminService.GetUserById(userId);
            return Ok(user);
        }

        // Admin only
        [HttpPut("UpdateUser/{userId:int}")]
        public async Task<IActionResult> UpdateUserDetails(int userId, [FromBody] UpdateUserModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _adminService.UpdateUserDetails(userId, model);
            return Ok("User updated successfully.");
        }

        // Admin only
        [HttpDelete("RemoveUser/{userId:int}")]
        public async Task<IActionResult> RemoveUser(int userId)
        {
            await _adminService.RemoveUser(userId);
            return Ok("User removed successfully.");
        }

        // Admin, Trainer
        [HttpGet("GetAllMembers")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllMembers()
        {
            var members = await _adminService.GetAllMembers();
            return Ok(members);
        }

        // Admin, Trainer, Member
        [HttpGet("GetAllTrainers")]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllTrainers()
        {
            var trainers = await _adminService.GetAllTrainers();
            return Ok(trainers);
        }
    }
}
