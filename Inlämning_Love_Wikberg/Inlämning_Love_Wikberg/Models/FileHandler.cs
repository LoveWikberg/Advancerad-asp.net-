using Inlämning_Love_Wikberg.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Inlämning_Love_Wikberg.Models
{
    public class FileHandler
    {
        public Customer[] GetAllCustomersFromTextFile()
        {
            string[] textFile = File
                    .ReadAllLines($@"{Directory.GetCurrentDirectory()}\wwwroot\TextFile.txt").ToArray();

            var allCustomers = textFile.Select(c => c.Split(',')).Select(c => new Customer
            {
                FirstName = c[0],
                LastName = c[1],
                Email = c[2],
                Gender = c[3],
                Age = int.Parse(c[4]),
                CreationDate = DateTime.Now,
            }).ToArray();

            return allCustomers;
        }

        public FileInfo[] GetAllFilesFromFolder(DirectoryInfo folder)
        {
            var logDirectory = folder.GetFiles();
            if (logDirectory == null)
                throw new Exception("Could not find any log files");
            return logDirectory;
        }
    }
}
