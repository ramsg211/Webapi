using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restfullApiDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restfullApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerContext _dbContext;

        public CustomerController(CustomerContext dbContext)
        {
            _dbContext = dbContext;
        }

        // api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            if(_dbContext.customers == null)
            {
                return NotFound();
            }

            return await _dbContext.customers.ToListAsync();
        }

        // api/Customer/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            if (_dbContext.customers == null)
            {
                return NotFound();
            }

            var customer = await _dbContext.customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // api/Customers
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _dbContext.customers.Add(customer);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }


        // PUT: api/Customer/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(customer).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }


        // api/Customer/2
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _dbContext.customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _dbContext.customers.Remove(customer);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }


        private bool CustomerExists(int id)
        {
            return (_dbContext.customers?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
