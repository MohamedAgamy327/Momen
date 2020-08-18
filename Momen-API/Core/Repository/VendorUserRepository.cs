using Data.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Core.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities.StaticHelpers;

namespace Core.Repository
{
    public class VendorUserRepository : IVendorUserRepository
    {
        private readonly ApplicationContext _context;
        public VendorUserRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<VendorUser> AddAsync(VendorUser vendorUser)
        {
            await _context.VendorsUsers.AddAsync(vendorUser);
            return vendorUser;
        }
        public VendorUser Edit(VendorUser vendorUser)
        {
            _context.Entry(vendorUser).State = EntityState.Modified;
            return vendorUser;
        }
        public async Task<VendorUser> GetAsync(int id)
        {
            return await _context.VendorsUsers.AsNoTracking().SingleOrDefaultAsync(s => s.Id == id);
        }
        public async Task<VendorUser> LoginAsync(string email, string password)
        {
            VendorUser vendorUser = await _context.VendorsUsers.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());

            if (vendorUser == null)
                return null;

            if (!SecurePassword.VerifyPasswordHash(password, vendorUser.PasswordHash, vendorUser.PasswordSalt))
                return null;

            return vendorUser;
        }
        public async Task<IEnumerable<VendorUser>> GetByVendorAsync(int vendorId)
        {
            return await _context.VendorsUsers.Where(w => w.VendorId == vendorId).OrderByDescending(o => o.Id).ToListAsync();
        }
        public void Remove(VendorUser vendorUser)
        {
            _context.Remove(vendorUser);
        }
        public async Task<bool> IsExist(int id)
        {
            return await _context.VendorsUsers.AnyAsync(s => s.Id == id).ConfigureAwait(true);
        }
        public async Task<bool> IsExistByVendor(int vendorId)
        {
            return await _context.VendorsUsers.AnyAsync(s => s.VendorId == vendorId).ConfigureAwait(true);
        }
        public async Task<bool> IsExistByPhone(string phone)
        {
            return await _context.VendorsUsers.AnyAsync(s => s.Phone == phone).ConfigureAwait(true);
        }
        public async Task<bool> IsExistByEmail(string email)
        {
            return await _context.VendorsUsers.AnyAsync(s => s.Email == email).ConfigureAwait(true);
        }
        public async Task<bool> IsExistByEmail(int id, string email)
        {
            return await _context.VendorsUsers.AnyAsync(s => s.Id != id && s.Email == email).ConfigureAwait(true);
        }
        public async Task<bool> IsExistByPhone(int id, string phone)
        {
            return await _context.VendorsUsers.AnyAsync(s => s.Id != id && s.Phone == phone).ConfigureAwait(true);
        }
    }
}
