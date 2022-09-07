using FluentValidation;
using PayCore_HW2.Extensions;
using PayCore_HW2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PayCore_HW2.FluentValidation
{
    public class EmployeeValidator:AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            // Id alanı boş olamaz

            RuleFor(x => x.Id).NotEmpty().WithMessage("Id field is required");

            // Name alanı 4 ve 120 karakter arasında olmalı özel karakter ve rakam içermemeli.
            // Türkçe karakter için regex tanımlanması yapılmıştır.

            RuleFor(x => x.Name).Length(4, 120).WithMessage("Name must be range in 4 - 120")
                    .Matches(@"^[a-zA-Z0ğüşöçİĞÜŞÖÇ ]+$").WithMessage("Invalid name input");

            // LastName alanı 4 ve 120 karakter arasında olmalı ve özel karakter içermemeli.
            // Türkçe karakter için regex tanımlanması yapılmıştır.

            RuleFor(x => x.LastName).Length(4, 120).WithMessage("LastName must be range in 4-120")
                    .Matches(@"^[a-zA-Z0ğüşöçİĞÜŞÖÇ]+$").WithMessage("Invalid lastname input");

            // DateOfBirth alanı  extension metotlarla kontrol edilmiştir.

            RuleFor(x => x.DateOfBirth).Must(EmployeeExtension.DateOfBirth).WithMessage("Please enter a value between 11/11/1945 and 10/10/2002");

            // E mail alanı extension metotlarla kontrol edilmiştir.

            RuleFor(x => x.Email).Must(EmployeeExtension.Email).WithMessage("Invalid E-mail adress");

            // PhoneNumber alanı min 7 max 15 karakter uzunluğunda olmalıdır(Bu bir standart)
            // PhoneNumber alanı extension metotlarla kontrol edilmiştir.

            RuleFor(x => x.PhoneNumber).Length(7, 15).Must(EmployeeExtension.PhoneNumber).WithMessage("Invalid Phone Number");

            //Salary alanı min 2000 max 9000 değer alıcak şekilde kontrol edilmiştir.

            RuleFor(x => x.Salary).GreaterThanOrEqualTo(2000).WithMessage("Please enter a value between 2000-9000").LessThanOrEqualTo(9000).WithMessage("Please enter a value between 2000-9000");
           
            
        }

      
    }
}
