namespace API.DTO.Contract
{
    public class ContractForGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PdfName { get; set; }
        public string PdfPath { get; set; }
    }
}
