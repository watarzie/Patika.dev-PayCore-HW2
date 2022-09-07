
using PayCore_HW2.FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace PayCore_HW2.Models
{
    // Employee Model Sınıfı
    public class Employee
    {
        
        public int Id { get; set; } 

        public string Name { get; set; }
   
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public int Salary { get; set; }
        
      
    }
}
