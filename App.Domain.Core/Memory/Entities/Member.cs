using App.Domain.Core.Memory.Entities.Enum;

namespace App.Domain.Core.Memory.Entities
{
    public class Member
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Score { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime BirthDate { get; set; }
        public string NationalCode { get; set; }
        public string? PhoneNumber { get; set; }
        public MembershipType MembershipType { get; set; }
        public Gender Gender { get; set; }
        public string? RegisterDateString { get; set; }
        public string? BirthDateString { get; set; }
    }
}
