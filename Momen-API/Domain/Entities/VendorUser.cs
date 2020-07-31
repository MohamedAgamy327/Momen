using Domain.Enums;

namespace Domain.Entities
{
    public class VendorUser : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool IsRandom { get; set; }
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }
        public VendorUserRoleEnum Role { get; set; }
    }
}
