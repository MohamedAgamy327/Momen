using Data.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Core.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repository
{
    public class VendorPictureRepository : IVendorPictureRepository
    {
        private readonly ApplicationContext _context;
        public VendorPictureRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<VendorPicture> AddAsync(VendorPicture vendorPicture)
        {
            await _context.VendorsPictures.AddAsync(vendorPicture);
            return vendorPicture;
        }
        public VendorPicture Edit(VendorPicture vendorPicture)
        {
            _context.Entry(vendorPicture).State = EntityState.Modified;
            return vendorPicture;
        }
        public async Task<VendorPicture> GetAsync(int id)
        {
            return await _context.VendorsPictures.AsNoTracking().SingleOrDefaultAsync(s => s.Id == id);
        }
        public async Task<IEnumerable<VendorPicture>> GetByVendorAsync(int vendorId)
        {
            return await _context.VendorsPictures.OrderByDescending(o => o.Id).Where(w => w.VendorId == vendorId).ToListAsync();
        }
        public void Remove(VendorPicture vendorPicture)
        {
            _context.Remove(vendorPicture);
        }
        public async Task<bool> IsExist(int id)
        {
            return await _context.VendorsPictures.AnyAsync(s => s.Id == id).ConfigureAwait(true);
        }
    }
}
