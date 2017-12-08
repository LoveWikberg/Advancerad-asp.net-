using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Inlämning_Love_Wikberg.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Inlämning_Love_Wikberg.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Inlämning_Love_Wikberg.Controllers
{
    [Route("api/customer")]
    public class CustomerController : Controller
    {
        DatabaseContext databaseContext;
        private readonly ILogger<CustomerController> logger;
        FileHandler fileHandler;

        public CustomerController(DatabaseContext databasecontext
            , ILogger<CustomerController> logger
            , FileHandler fileHandler)
        {
            this.logger = logger;
            this.fileHandler = fileHandler;
            this.databaseContext = databasecontext;
            databasecontext.Database.EnsureCreated();
        }

        [HttpPost]
        public IActionResult AddCustomer(Customer customer)
        {
            try
            {
                customer.CreationDate = DateTime.Now;
                databaseContext.Add(customer);
                databaseContext.SaveChanges();
                logger.LogInformation("Lagt till en kund");
                return Ok("Customer added");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public IActionResult EditCustomer(Customer customer)
        {
            try
            {
                customer.MostRecentUpdate = DateTime.Now;
                //Prevent CreationDate from being modified
                databaseContext.Entry(customer).State = EntityState.Modified;
                databaseContext.Entry(customer).Property(x => x.CreationDate).IsModified = false;

                databaseContext.SaveChanges();
                return Ok("Customer edited");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteCustomer(int id)
        {
            try
            {
                var customer = databaseContext.GetCustomerById(id);
                databaseContext.Remove(customer);
                databaseContext.SaveChanges();
                return Ok("Customer deleted");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllCustomers()
        {
            try
            {
                logger.LogInformation("Hämtat alla kunder");
                return Ok(databaseContext.Customers);
            }
            catch (Exception e)
            {
                logger.LogError(new EventId(), e, "Fail att 'GetAllCustomers'");
                return BadRequest(e.Message);
            }
        }

        [HttpPost, Route("replaceDatabaseCustomersWithTextFileCustomers")]
        public IActionResult ReplaceAllDatabaseCustomersWithTextFileCustomers()
        {
            try
            {
                databaseContext.RemoveAllCustomers();
                databaseContext.AddContentToDatabase(fileHandler.GetAllCustomersFromTextFile());
                return Ok("Database content successfully replaced with textfile content.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
