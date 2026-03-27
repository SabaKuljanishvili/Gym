namespace GymMembershipManagement.SERVICE.DTOs.Membership
{
    public class MembershipDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MembershipTypeId { get; set; }
        public string MembershipTypeName { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}
