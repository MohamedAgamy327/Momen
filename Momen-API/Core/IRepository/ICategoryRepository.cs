using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.IRepository
{
    public interface ICategoryRepository
    {
        Task<Category> AddAsync(Category category);
        Category Edit(Category category);
        void Remove(Category category);
        Task<Category> GetAsync(int id);
        Task<IEnumerable<Category>> GetAsync();
        Task<bool> IsExist(int id);
        Task<bool> IsExist(string name);
        Task<bool> IsExist(int id, string name);
    }
}
