using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface IVendorUserRepository
    {
        Task<VendorUser> AddAsync(VendorUser vendorUser);
        VendorUser Edit(VendorUser vendorUser);
        void Remove(VendorUser vendorUser);
        Task<VendorUser> GetAsync(int id);
        Task<VendorUser> LoginAsync(string email, string password);
        Task<IEnumerable<VendorUser>> GetAsync();
        Task<bool> IsExist(int id);
        Task<bool> IsExistByVendor(int vendorId);
        Task<bool> IsExistByEmail(string email);
        Task<bool> IsExistByPhone(string phone);
        Task<bool> IsExistByEmail(int id, string email);
        Task<bool> IsExistByPhone(int id, string phone);
    }
}