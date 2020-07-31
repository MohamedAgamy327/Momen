using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface IVendorRepository
    {
        Task<Vendor> AddAsync(Vendor vendor);
        Vendor Edit(Vendor vendor);
        void Remove(Vendor vendor);
        Task<Vendor> GetAsync(int id);
        Task<IEnumerable<Vendor>> GetAsync();
        Task<bool> IsExist(int id);
        Task<bool> IsExistByCategory(int categoryId);
        Task<bool> IsExistByEmail(string email);
        Task<bool> IsExistByPhone(string phone);
        Task<bool> IsExistByEmail(int id, string email);
        Task<bool> IsExistByPhone(int id, string phone);
        Task<bool> IsExist(string name, int categoryId);
        Task<bool> IsExist(int id, string name, int categoryId);
    }
}