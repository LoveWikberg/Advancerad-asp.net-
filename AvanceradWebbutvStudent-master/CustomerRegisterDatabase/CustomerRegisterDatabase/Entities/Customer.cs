﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerRegisterDatabase.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public int Age { get; set; }
        public DateTime Created { get; set; }
        public DateTime MostRecentUpdate { get; set; }

    }
}
