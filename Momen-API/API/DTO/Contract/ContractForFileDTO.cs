using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.DTO.Contract
{
    public class ContractForFileDTO
    {
        [FromForm(Name = "Id")]
        public int Id { get; set; }

        [FromForm(Name = "pdf")]
        public IFormFile Pdf { get; set; }
    }
}
