using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface IVendorPictureRepository
    {
        Task<VendorPicture> AddAsync(VendorPicture vendorPicture);
        VendorPicture Edit(VendorPicture vendorPicture);
        void Remove(VendorPicture vendorPicture);
        Task<VendorPicture> GetAsync(int id);
        Task<IEnumerable<VendorPicture>> GetByVendorAsync(int vendorId);
        Task<bool> IsExist(int id);
    }
}
