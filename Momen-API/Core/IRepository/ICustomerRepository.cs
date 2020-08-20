using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface ICustomerRepository
    {
        Task<Customer> AddAsync(Customer customer);
        Customer Edit(Customer customer);
        void Remove(Customer customer);
        Task<Customer> GetAsync(int id);
        Task<Customer> LoginAsync(string email, string password);
        Task<IEnumerable<Customer>> GetAsync();
        Task<bool> IsExist(int id);
        Task<bool> IsExistByEmail(string email);
        Task<bool> IsExistByPhone(string phone);
        Task<bool> IsExistByEmail(int id, string email);
        Task<bool> IsExistByPhone(int id, string phone);
    }
}