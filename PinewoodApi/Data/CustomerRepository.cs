using PinewoodApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace PinewoodApi.Data
{
    public class CustomerRepository
    {
        private static List<Customer> _customers = new List<Customer>
        {
            new Customer { Id = 1, Name = "Test 01", Email = "test1@user.com", Phone = "00442079460958" },
            new Customer { Id = 2, Name = "Test 02", Email = "test2@user.com", Phone = "00447712345678" }
        };

        public List<Customer> GetAll() => _customers;

        public Customer GetById(int id) => _customers.FirstOrDefault(c => c.Id == id);

        public void Add(Customer customer)
        {
            customer.Id = _customers.Max(c => c.Id) + 1;
            _customers.Add(customer);
        }

        public void Update(Customer customer)
        {
            var existing = GetById(customer.Id);
            if (existing != null)
            {
                existing.Name = customer.Name;
                existing.Email = customer.Email;
                existing.Phone = customer.Phone;
            }
        }

        public void Delete(int id)
        {
            var customer = GetById(id);
            if (customer != null)
            {
                _customers.Remove(customer);
            }
        }
    }
}
