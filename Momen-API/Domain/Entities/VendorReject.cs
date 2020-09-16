using System;

namespace Domain.Entities
{
    public class VendorReject : BaseEntity
    {
        public DateTime CreationDate { get; set; }
        public string Reason { get; set; }
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }
    }
}
