namespace API.DTO.VendorUser
{
    public class VendorUserForChangePasswordDTO
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
