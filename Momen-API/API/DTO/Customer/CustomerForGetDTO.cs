namespace API.DTO.Customer
{
    public class CustomerForGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string IsBlocked { get; set; }
        public string PictureName { get; set; }
        public string PicturePath { get; set; }
    }
}
