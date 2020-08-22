namespace API.DTO.Vendor
{
    public class VendorForAddDTO
    {
        public string Name { get; set; }
        public int BranchesCount { get; set; }
        public string Description { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public int CategoryId { get; set; }
        public UserForVendorAddDTO VendorUser { get; set; }
    }
}
