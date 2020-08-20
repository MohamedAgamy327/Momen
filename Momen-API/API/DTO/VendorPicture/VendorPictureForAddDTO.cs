using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.DTO.VendorPicture
{
    public class VendorPictureForAddDTO
    {
        [FromForm(Name = "pictures")]
        public IFormFileCollection Pictures { get; set; }

        [FromForm(Name = "vendorId")]
        public int VendorId { get; set; }
    }
}