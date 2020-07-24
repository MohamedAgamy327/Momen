using Data.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Core.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repository
{
    public class ContractRepository : IContractRepository
    {
        private readonly ApplicationContext _context;
        public ContractRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Contract> AddAsync(Contract contract)
        {
            await _context.Contracts.AddAsync(contract);
            return contract;
        }
        public Contract Edit(Contract contract)
        {
            _context.Entry(contract).State = EntityState.Modified;
            return contract;
        }
        public async Task<Contract> GetAsync(int id)
        {
            return await _context.Contracts.AsNoTracking().SingleOrDefaultAsync(s => s.Id == id);
        }
        public async Task<IEnumerable<Contract>> GetAsync()
        {
            return await _context.Contracts.OrderByDescending(o => o.Id).ToListAsync();
        }
        public void Remove(Contract contract)
        {
            _context.Remove(contract);
        }
        public async Task<bool> IsExist(int id)
        {
            return await _context.Contracts.AnyAsync(s => s.Id == id).ConfigureAwait(true);
        }
        public async Task<bool> IsExist(string name)
        {
            return await _context.Contracts.AnyAsync(s => s.Name.ToLower() == name.ToLower()).ConfigureAwait(true);
        }
        public async Task<bool> IsExist(int id, string name)
        {
            return await _context.Contracts.AnyAsync(s => s.Id != id && s.Name.ToLower() == name.ToLower()).ConfigureAwait(true);
        }
    }
}
