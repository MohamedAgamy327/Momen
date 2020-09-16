using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface IVendorRejectRepository
    {
        Task<VendorReject> AddAsync(VendorReject vendorReject);
        VendorReject Edit(VendorReject vendorReject);
        void Remove(VendorReject vendorReject);
        Task<VendorReject> GetAsync(int id);
        Task<IEnumerable<VendorReject>> GetByVendorAsync(int vendorId);
        Task<bool> IsExist(int id);
    }
}
