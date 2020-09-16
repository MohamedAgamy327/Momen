using Data.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Core.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repository
{
    public class VendorRejectRepository : IVendorRejectRepository
    {
        private readonly ApplicationContext _context;
        public VendorRejectRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<VendorReject> AddAsync(VendorReject vendorReject)
        {
            await _context.VendorsRejects.AddAsync(vendorReject);
            return vendorReject;
        }
        public VendorReject Edit(VendorReject vendorReject)
        {
            _context.Entry(vendorReject).State = EntityState.Modified;
            return vendorReject;
        }
        public async Task<VendorReject> GetAsync(int id)
        {
            return await _context.VendorsRejects.AsNoTracking().SingleOrDefaultAsync(s => s.Id == id);
        }
        public async Task<IEnumerable<VendorReject>> GetByVendorAsync(int vendorId)
        {
            return await _context.VendorsRejects.OrderByDescending(o => o.Id).Where(w => w.VendorId == vendorId).ToListAsync();
        }
        public void Remove(VendorReject vendorReject)
        {
            _context.Remove(vendorReject);
        }
        public async Task<bool> IsExist(int id)
        {
            return await _context.VendorsRejects.AnyAsync(s => s.Id == id).ConfigureAwait(true);
        }
    }
}
