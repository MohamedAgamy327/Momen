namespace API.DTO.Customer
{
    public class CustomerForChangePasswordDTO
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
