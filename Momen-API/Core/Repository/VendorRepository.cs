using Data.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Core.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repository
{
    public class VendorRepository : IVendorRepository
    {
        private readonly ApplicationContext _context;
        public VendorRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Vendor> AddAsync(Vendor vendor)
        {
            await _context.Vendors.AddAsync(vendor);
            return vendor;
        }
        public Vendor Edit(Vendor vendor)
        {
            _context.Entry(vendor).State = EntityState.Modified;
            return vendor;
        }
        public async Task<Vendor> GetAsync(int id)
        {
            return await _context.Vendors.Include(k => k.Category).AsNoTracking().SingleOrDefaultAsync(s => s.Id == id);
        }
        public async Task<IEnumerable<Vendor>> GetAsync()
        {
            return await _context.Vendors.OrderByDescending(o => o.Id).Include(k => k.Category).ToListAsync();
        }
        public void Remove(Vendor vendor)
        {
            _context.Remove(vendor);
        }
        public async Task<bool> IsExist(int id)
        {
            return await _context.Vendors.AnyAsync(s => s.Id == id).ConfigureAwait(true);
        }
        public async Task<bool> IsExistByCategory(int categoryId)
        {
            return await _context.Vendors.AnyAsync(s => s.CategoryId == categoryId).ConfigureAwait(true);
        }
        public async Task<bool> IsExistByPhone(string phone)
        {
            return await _context.Vendors.AnyAsync(s => s.Phone == phone).ConfigureAwait(true);
        }
        public async Task<bool> IsExistByEmail(string email)
        {
            return await _context.Vendors.AnyAsync(s => s.Email == email).ConfigureAwait(true);
        }
        public async Task<bool> IsExist(string name, int categoryId)
        {
            return await _context.Vendors.AnyAsync(s => s.Name.ToLower() == name.ToLower() && s.CategoryId == categoryId).ConfigureAwait(true);
        }
        public async Task<bool> IsExistByEmail(int id, string email)
        {
            return await _context.Vendors.AnyAsync(s => s.Id != id && s.Email == email).ConfigureAwait(true);
        }
        public async Task<bool> IsExistByPhone(int id, string phone)
        {
            return await _context.Vendors.AnyAsync(s => s.Id != id && s.Phone == phone).ConfigureAwait(true);
        }
        public async Task<bool> IsExist(int id, string name, int categoryId)
        {
            return await _context.Vendors.AnyAsync(s => s.Id != id && s.Name.ToLower() == name.ToLower() && s.CategoryId == categoryId).ConfigureAwait(true);
        }
    }
}
