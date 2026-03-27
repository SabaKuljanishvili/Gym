using GymMembershipManagement.SERVICE.DTOs.Membership;
using GymMembershipManagement.SERVICE.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymMembershipManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipController : ControllerBase
    {
        private readonly IMembershipService _membershipService;

        public MembershipController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        // Admin only
        [HttpPost("Register")]
        public async Task<ActionResult<MembershipDTO>> RegisterMembership([FromBody] RegisterMembershipDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _membershipService.RegisterMembership(dto);
            return Ok(result);
        }

        // Admin only
        [HttpPut("Renew/{membershipId:int}")]
        public async Task<ActionResult<bool>> RenewMembership(int membershipId)
        {
            var result = await _membershipService.RenewMembership(membershipId);
            if (!result) return NotFound("Membership not found.");
            return Ok(result);
        }

        // Admin, Trainer, Member
        [HttpGet("Status/{customerId:int}")]
        public async Task<ActionResult<MembershipStatusDTO>> GetMembershipStatus(int customerId)
        {
            var status = await _membershipService.GetMembershipStatus(customerId);
            return Ok(status);
        }

        // Admin, Trainer
        [HttpGet("ByUser/{userId:int}")]
        public async Task<ActionResult<IEnumerable<MembershipDTO>>> GetMembershipsByUser(int userId)
        {
            var memberships = await _membershipService.GetMembershipsByUser(userId);
            return Ok(memberships);
        }

        // Admin, Trainer
        [HttpGet("Active")]
        public async Task<ActionResult<IEnumerable<MembershipDTO>>> GetMembershipsByStatus()
        {
            var memberships = await _membershipService.GetMembershipsByStatus();
            return Ok(memberships);
        }
    }
}
