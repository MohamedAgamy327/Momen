namespace Domain.Entities
{
    public class VendorPicture : BaseEntity
    {
        public string PictureName { get; set; }
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }
    }
}
