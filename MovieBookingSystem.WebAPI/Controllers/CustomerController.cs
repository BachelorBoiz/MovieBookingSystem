using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieBookingSystem.Core.Models;
using MovieBookingSystem.Domain.IRepositories;

namespace MovieBookingSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        
        // GET: api/Customer
        [HttpGet]
        public IEnumerable<Customer> GetAll()
        {
            return _customerRepository.GetCustomers();
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var customer = _customerRepository.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return new ObjectResult(customer);
        }

        // POST: api/Customer
        [HttpPost]
        public IActionResult Post([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }

            return new ObjectResult(_customerRepository.CreateCustomer(customer));
        }

        // PUT: api/Customer/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Customer customer)
        {
            if (customer == null || customer.Id != id)
            {
                return BadRequest();
            }
            return new ObjectResult(_customerRepository.UpdateCustomer(customer));
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_customerRepository.GetCustomerById(id) == null)
            {
                return NotFound();
            }
            _customerRepository.DeleteCustomer(id);
            return NoContent();
        }
    }
}
