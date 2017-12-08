using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerRegisterDatabase.Entities
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> context) : base(context)
        {

        }

        public Customer GetCustomer(int id)
        {
            var customerToRemove = Customers.SingleOrDefault(c => c.Id == id);
            if (customerToRemove == null)
                throw new Exception();
            return customerToRemove;
        }

    }
}
