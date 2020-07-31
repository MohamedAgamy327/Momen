namespace API.DTO.VendorUser
{
    public class VendorUserForAddDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int VendorId { get; set; }
        public string Role { get; set; }
    }
}
