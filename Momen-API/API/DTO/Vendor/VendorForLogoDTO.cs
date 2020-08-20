using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.DTO.Vendor
{
    public class VendorForLogoDTO
    {
        [FromForm(Name = "Id")]
        public int Id { get; set; }

        [FromForm(Name = "logo")]
        public IFormFile Logo { get; set; }
    }
}
