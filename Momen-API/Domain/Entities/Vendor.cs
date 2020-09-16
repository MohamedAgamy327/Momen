using Domain.Enums;
using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Vendor : BaseEntity
    {
        public Vendor()
        {
            VendorUsers = new List<VendorUser>();
        }
        public DateTime CreationDate { get; set; }
        public string Name { get; set; }
        public int BranchesCount { get; set; }
        public string Description { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string LogoName { get; set; }
        public string LicenseName { get; set; }
        public string PersonalIdName { get; set; } 
        public VendorStatusEnum Status { get; set; }
        public int? ContractId { get; set; }
        public Contract Contract { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<VendorUser> VendorUsers { get; set; }
        public ICollection<VendorReject> VendorRejects { get; set; }
        public ICollection<VendorPicture> VendorPictures { get; set; }
    }
}
