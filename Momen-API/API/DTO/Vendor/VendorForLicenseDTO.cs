using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.DTO.Vendor
{
    public class VendorForLicenseDTO
    {
        [FromForm(Name = "Id")]
        public int Id { get; set; }

        [FromForm(Name = "license")]
        public IFormFile License { get; set; }
    }
}
