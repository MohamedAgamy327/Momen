using System.Collections.Generic;

namespace Domain.Entities
{
    public class Contract : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PdfName { get; set; }
        public ICollection<Vendor> Vendors { get; set; }
    }
}
