using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.DTO.Vendor
{
    public class VendorForPersonalIdDTO
    {
        [FromForm(Name = "Id")]
        public int Id { get; set; }

        [FromForm(Name = "file")]
        public IFormFile File { get; set; }
    }
}
