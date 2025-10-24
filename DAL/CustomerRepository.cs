using Models;

namespace DAL
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(InMemoryDatabase database) : base(database)
        {
        }

        public Customer? GetByEmail(string email)
        {
            return _dbSet.FirstOrDefault(c => c.EmailAddress == email && c.CustomerStatus == 1);
        }

        public IEnumerable<Customer> GetActiveCustomers()
        {
            return _dbSet.Where(c => c.CustomerStatus == 1).ToList();
        }

        public async Task<Customer?> GetByEmailAsync(string email)
        {
            await Task.CompletedTask;
            return GetByEmail(email);
        }
    }
}
