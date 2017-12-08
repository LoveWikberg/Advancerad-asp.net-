using Inlämning_Love_Wikberg.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inlämning_Love_Wikberg.Entities
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> context) : base(context)
        {

        }

        public Customer GetCustomerById(int id)
        {
            var customer = Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                throw new Exception();
            return customer;
        }

        public void RemoveAllCustomers()
        {
            //For better performance use "Database.ExecuteSqlCommand("TRUNCATE TABLE [TableName]");" 
            //to clear database. ***This will not save any work to the transaction log***.
            Customers.RemoveRange(Customers);
            SaveChanges();
        }

        public void AddContentToDatabase(Customer[] content)
        {
            Customers.AddRange(content);
            SaveChanges();
        }

    }
}
