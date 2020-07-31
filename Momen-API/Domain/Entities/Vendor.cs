using System.Collections.Generic;

namespace Domain.Entities
{
    public class Vendor : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int BranchesCount { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Map { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string LogoFileName { get; set; }
        public string LicenseFileName { get; set; }
        public string PersonalIdFileName { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<VendorUser> VendorUsers { get; set; }
    }
}
