using GymMembershipManagement.SERVICE.DTOs.GymClass;
using GymMembershipManagement.SERVICE.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GymMembershipManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymClassController : ControllerBase
    {
        private readonly IGymClassService _gymClassService;

        public GymClassController(IGymClassService gymClassService)
        {
            _gymClassService = gymClassService;
        }

        [HttpGet("GetAllGymClasses")]
        public async Task<ActionResult<IEnumerable<GymClassDto>>> GetAllGymClasses()
        {
            var classes = await _gymClassService.GetAllGymClassesAsync();
            return Ok(classes);
        }

        [HttpGet("GetGymClassById/{id:int}")]
        public async Task<ActionResult<GymClassDto>> GetGymClassById(int id)
        {
            if (id <= 0) return BadRequest("Invalid GymClass id");

            var gymClass = await _gymClassService.GetGymClassByIdAsync(id);
            return Ok(gymClass);
        }

        [HttpPost("AddGymClass")]
        public async Task<IActionResult> AddGymClass(CreateGymClassDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _gymClassService.CreateGymClassAsync(model);
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPut("UpdateGymClass/{id:int}")]
        public async Task<IActionResult> UpdateGymClass(int id, UpdateGymClassDto model)
        {
            if (ModelState.IsValid)
            {
                var result = await _gymClassService.UpdateGymClassAsync(id, model);
                if (!result) return NotFound("GymClass not found");
                return StatusCode(201);
            }

            return BadRequest();
        }

        [HttpDelete("DeleteGymClass/{id:int}")]
        public async Task<IActionResult> DeleteGymClass(int id)
        {
            if (id > 0)
            {
                var result = await _gymClassService.DeleteGymClassAsync(id);
                if (!result) return NotFound("GymClass not found");
                return Ok();
            }

            return BadRequest("Invalid GymClass id");
        }

}
}
