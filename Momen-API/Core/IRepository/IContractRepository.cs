using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface IContractRepository
    {
        Task<Contract> AddAsync(Contract contract);
        Contract Edit(Contract contract);
        void Remove(Contract contract);
        Task<Contract> GetAsync(int id);
        Task<IEnumerable<Contract>> GetAsync();
        Task<bool> IsExist(int id);
        Task<bool> IsExist(string name);
        Task<bool> IsExist(int id, string name);
    }
}
