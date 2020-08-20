using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.DTO.Customer
{
    public class CustomerForProfilePictureDTO
    {
        [FromForm(Name = "Id")]
        public int Id { get; set; }

        [FromForm(Name = "file")]
        public IFormFile File { get; set; }
    }
}
