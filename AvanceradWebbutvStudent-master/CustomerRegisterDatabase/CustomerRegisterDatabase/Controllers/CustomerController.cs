using CustomerRegisterDatabase.Entities;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CustomerRegisterDatabase.Controllers
{
    [Route("api/customers")]
    public class CustomerController : Controller
    {
        private DatabaseContext databaseContext;

        public CustomerController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
            databaseContext.Database.EnsureCreated();
        }

        [HttpPost]
        public IActionResult Add(Customer customer)
        {
            try
            {
                customer.Created = DateTime.Now;
                databaseContext.Add(customer);
                databaseContext.SaveChanges();
                return Ok($"{customer.FirstName} added");
            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}");
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                var customerToRemove = databaseContext.GetCustomer(id);
                databaseContext.Remove(customerToRemove);
                databaseContext.SaveChanges();
                return Ok($"{customerToRemove.FirstName} Deleted");
            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}");
            }
        }

        [HttpPut]
        public IActionResult Edit(Customer customer)
        {
            try
            {
                customer.MostRecentUpdate = DateTime.Now;
                databaseContext.Update(customer);
                databaseContext.SaveChanges();
                return Ok($"{customer.FirstName} edited");
            }
            catch (Exception e)
            {
                return BadRequest($"Error: {e.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            try
            {
                return Ok(databaseContext.Customers);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

    }
}
