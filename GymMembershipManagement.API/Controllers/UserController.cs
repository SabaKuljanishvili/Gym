using GymMembershipManagement.SERVICE.DTOs.User;
using GymMembershipManagement.SERVICE.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymMembershipManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Unauthorized
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _userService.UserRegistration(model);
            return Ok("User registered successfully.");
        }

        // Unauthorized
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = await _userService.Login(model.Email, model.Password);
            return Ok(user);
        }

        // Admin, Trainer, Member
        [HttpGet("GetProfile/{userId:int}")]
        public async Task<ActionResult<UserDTO>> GetProfile(int userId)
        {
            var user = await _userService.GetProfile(userId);
            return Ok(user);
        }

        // Admin, Trainer, Member
        [HttpPut("UpdateProfile/{userId:int}")]
        public async Task<ActionResult<UserDTO>> UpdateProfile(int userId, [FromBody] UpdateUserModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updatedUser = await _userService.UpdateProfile(userId, model);
            return Ok(updatedUser);
        }

        // Admin, Trainer, Member
        [HttpDelete("DeleteProfile/{userId:int}")]
        public async Task<IActionResult> DeleteProfile(int userId)
        {
            await _userService.DeleteProfile(userId);
            return Ok("User deleted successfully.");
        }

        // Admin, Trainer, Member
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            _userService.Logout();
            return Ok("Logged out successfully.");
        }
    }
}
