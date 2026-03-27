namespace GymMembershipManagement.SERVICE.DTOs.Membership
{
    public class MembershipStatusDTO
    {
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string MembershipTypeName { get; set; } = null!;
    }
}
