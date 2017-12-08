using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LabbFem.Models.ExtensionMethods;

namespace LabbFem.Models
{
    public class DataHandler
    {
        public CustomerVM[] GetAllCustomersFromTextFile()
        {
            string[] textFile = File
                    .ReadAllLines($@"{Directory.GetCurrentDirectory()}\wwwroot\TextFile.txt").ToArray();

            var allCustomers = textFile.Select(c => c.Split(',')).Select(c => new CustomerVM
            {
                Id = int.Parse(c[0]),
                FirstName = c[1],
                LastName = c[2],
                Email = c[3],
                Gender = c[4],
                Age = int.Parse(c[5]),
                ImageFileName = c[6]
            }).ToArray();

            return allCustomers;
        }

        public void AddCustomerToTextFile(AddCustomerVM viewModel)
        {
            var customers = GetAllCustomersFromTextFile().ToList();
            int highestId = customers.Select(i => i.Id).Max();

            customers.Add(new CustomerVM
            {
                Id = highestId + 1,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                Gender = viewModel.Gender,
                Age = viewModel.Age,
                ImageFileName = "cillinmuprth.jpg"
            });

            //string contentToAdd = "";

            //for (int i = 0; i < customers.Count; i++)
            //{
            //    if (i != 0)
            //        contentToAdd += $"{Environment.NewLine}{customers[i].Id.ToString()},{customers[i].FirstName},{customers[i].LastName},{customers[i].Email},{customers[i].Gender},{customers[i].Age},{customers[i].ImageFileName}";
            //    else
            //        contentToAdd += $"{customers[i].Id.ToString()},{customers[i].FirstName},{customers[i].LastName},{customers[i].Email},{customers[i].Gender},{customers[i].Age},{customers[i].ImageFileName}";
            //}
            File.WriteAllText($@"{Directory.GetCurrentDirectory()}\wwwroot\TextFile.txt", customers.ToArray().ConvertContentToTextFileString());
        }

        public T GetOneCustomer<T>(ApiCall apicall)
        {
            var customer = GetAllCustomersFromTextFile().SingleOrDefault(c => c.Id == apicall.Id);
            if (customer == null)
                throw new NullReferenceException();
            if (!apicall.IsBrief)
            {
                return (T)(object)customer;
            }
            else
            {
                var briefCustomer = new BriefCustomer
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName
                };
                return (T)(object)briefCustomer;
            }
        }

        public void RemoveLineFromTextFile(int id)
        {
            var textFile = File
                   .ReadAllLines($@"{Directory.GetCurrentDirectory()}\wwwroot\TextFile.txt").ToList();
            var customerToRemove = textFile.SingleOrDefault(t => int.Parse(t.Split(',')[0]) == id);
            textFile.Remove(customerToRemove);

            File.WriteAllLines($@"{Directory.GetCurrentDirectory()}\wwwroot\TextFile.txt", textFile);
        }

        public void EditCustomerInTextFile(CustomerVM viewModel)
        {
            var customers = GetAllCustomersFromTextFile();
            var customerToEdit = customers.SingleOrDefault(t => t.Id == viewModel.Id);

            customerToEdit.FirstName = viewModel.FirstName;
            customerToEdit.LastName = viewModel.LastName;
            customerToEdit.Email = viewModel.Email;
            customerToEdit.Gender = viewModel.Gender;
            customerToEdit.Age = viewModel.Age;
            customerToEdit.ImageFileName = "cillinmuprth.jpg";
            //customerToEdit.ImageFileName = viewModel.ImageFileName;

            File.WriteAllText($@"{Directory.GetCurrentDirectory()}\wwwroot\TextFile.txt", customers.ConvertContentToTextFileString());
        }
    }
}
