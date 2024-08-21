using Microsoft.AspNetCore.Mvc;
using PinewoodApi.Data;
using PinewoodApi.Models;
using System.Collections.Generic;

namespace PinewoodApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerRepository _repository;

        public CustomerController()
        {
            _repository = new CustomerRepository();
        }

        [HttpGet]
        public ActionResult<List<Customer>> GetAllCustomers()
        {
            return _repository.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomerById(int id)
        {
            var customer = _repository.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return customer;
        }

        [HttpPost]
        public IActionResult AddCustomer(Customer customer)
        {
            _repository.Add(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            var existingCustomer = _repository.GetById(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }

            _repository.Update(customer);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _repository.GetById(id);
            if (customer == null)
            {
                return NotFound();
            }

            _repository.Delete(id);
            return NoContent();
        }
    }
}
