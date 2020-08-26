using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.DTO.Customer
{
    public class CustomerForPictureDTO
    {
        [FromForm(Name = "Id")]
        public int Id { get; set; }

        [FromForm(Name = "picture")]
        public IFormFile Picture { get; set; }
    }
}
