using Data.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Core.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationContext _context;
        public CategoryRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Category> AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            return category;
        }
        public Category Edit(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            return category;
        }
        public async Task<Category> GetAsync(int id)
        {
            return await _context.Categories.AsNoTracking().SingleOrDefaultAsync(s => s.Id == id);
        }
        public async Task<IEnumerable<Category>> GetAsync()
        {
            return await _context.Categories.OrderByDescending(o => o.Id).ToListAsync();
        }
        public void Remove(Category category)
        {
            _context.Remove(category);
        }
        public async Task<bool> IsExist(int id)
        {
            return await _context.Categories.AnyAsync(s => s.Id == id).ConfigureAwait(true);
        }
        public async Task<bool> IsExist(string name)
        {
            return await _context.Categories.AnyAsync(s => s.Name.ToLower() == name.ToLower()).ConfigureAwait(true);
        }
        public async Task<bool> IsExist(int id, string name)
        {
            return await _context.Categories.AnyAsync(s => s.Id != id && s.Name.ToLower() == name.ToLower()).ConfigureAwait(true);
        }
    }
}
