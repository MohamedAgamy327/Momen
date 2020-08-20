namespace Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool IsRandom { get; set; }
        public bool IsBlocked { get; set; }
        public string ProfilePictureName { get; set; }
    }
}
