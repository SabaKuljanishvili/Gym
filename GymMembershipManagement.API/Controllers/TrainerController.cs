using GymMembershipManagement.SERVICE.DTOs.Schedule;
using GymMembershipManagement.SERVICE.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymMembershipManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainerController : ControllerBase
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        // Admin only
        [HttpPost("AssignSchedule")]
        public async Task<ActionResult<bool>> AssignSchedule([FromBody] AssignScheduleDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _trainerService.AssignSchedule(dto);
            return Ok(result);
        }

        // Admin only
        [HttpGet("Schedules/{trainerId:int}")]
        public async Task<ActionResult<IEnumerable<ScheduleDTO>>> GetSchedulesByTrainer(int trainerId)
        {
            var schedules = await _trainerService.GetSchedulesByTrainer(trainerId);
            return Ok(schedules);
        }
    }
}
