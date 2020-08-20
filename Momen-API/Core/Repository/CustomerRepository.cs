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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationContext _context;
        public CustomerRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Customer> AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            return customer;
        }
        public Customer Edit(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            return customer;
        }
        public async Task<Customer> GetAsync(int id)
        {
            return await _context.Customers.AsNoTracking().SingleOrDefaultAsync(s => s.Id == id);
        }
        public async Task<Customer> LoginAsync(string email, string password)
        {
            Customer customer = await _context.Customers.FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower() && x.IsBlocked == false);

            if (customer == null)
                return null;

            if (!SecurePassword.VerifyPasswordHash(password, customer.PasswordHash, customer.PasswordSalt))
                return null;

            return customer;
        }
        public async Task<IEnumerable<Customer>> GetAsync()
        {
            return await _context.Customers.OrderByDescending(o => o.Id).ToListAsync();
        }
        public void Remove(Customer customer)
        {
            _context.Remove(customer);
        }
        public async Task<bool> IsExist(int id)
        {
            return await _context.Customers.AnyAsync(s => s.Id == id).ConfigureAwait(true);
        }
        public async Task<bool> IsExistByPhone(string phone)
        {
            return await _context.Customers.AnyAsync(s => s.Phone == phone).ConfigureAwait(true);
        }
        public async Task<bool> IsExistByEmail(string email)
        {
            return await _context.Customers.AnyAsync(s => s.Email == email).ConfigureAwait(true);
        }
        public async Task<bool> IsExistByEmail(int id, string email)
        {
            return await _context.Customers.AnyAsync(s => s.Id != id && s.Email == email).ConfigureAwait(true);
        }
        public async Task<bool> IsExistByPhone(int id, string phone)
        {
            return await _context.Customers.AnyAsync(s => s.Id != id && s.Phone == phone).ConfigureAwait(true);
        }
    }
}
