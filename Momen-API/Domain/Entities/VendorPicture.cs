namespace Domain.Entities
{
    public class VendorPicture : BaseEntity
    {
        public string FileName { get; set; }
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }
    }
}
