using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayCore_HW2.FluentValidation;
using PayCore_HW2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayCore_HW2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EmployeeController : ControllerBase
    {
        private static List<Employee> _list;
        
        // Constructor
        static EmployeeController()
        {
            _list = new List<Employee> // Kurucuda statik olarak liste oluşturulur.
           {
               new Employee
               {
                   Id=1,
                   Name="Ömer",
                   LastName="Akkaya",
                   DateOfBirth=new DateTime(1997,11,24),
                   Email="omerakkaya@outlook.com",
                   PhoneNumber="+905301449987",
                   Salary=5500

               },
               new Employee
               {
                   Id=2,
                   Name="Kerem",
                   LastName="Akkaya",
                   DateOfBirth=new DateTime(2001,12,01),
                   Email="keremakkaya@outlook.com",
                   PhoneNumber="+905462345676",
                   Salary=5500

               }
           };
        }

        // Employee listesini geri döndüren Action Metot.

        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employeeList = _list.OrderBy(x => x.Id).ToList<Employee>();
            return Ok(employeeList);
            
        }

        // Employee nesnesini Employee listesinden id ile filtreleyip geri döndüren Action Metot.

        [HttpGet("{id}")]
        public IActionResult GetById(int id) // Route'dan alır.
        {
            var employee = _list.SingleOrDefault(x => x.Id == id);
            if(employee is null)
            {
                return BadRequest("Çalışan bulunamadı");
            }
            return Ok(employee);
        }
        
        // Employee eklememizi sağlayan Action Metot

        [HttpPost]
        [Route("CreateEmployee")]
        public IActionResult CreateEmployee([FromBody] Employee createEmployee) // Değerleri Body'den alır.
        {
            var employee = _list.SingleOrDefault(x => x.Id == createEmployee.Id);
            if(employee is not null) // employee boş değil ise aynı id'ye sahip başka kayıtın olduğunu döndürür.
            {
                return BadRequest("Bu kayıt sistemde mevcut");
            }
            // Fluent Validation'ı çağırdığımız yer.
            try
            {
                EmployeeValidator validator = new EmployeeValidator(); //Employeevalidator nesnesi oluşturulur.
                validator.ValidateAndThrow(createEmployee); // validasyon gerçekleşirse ekler gerçekleşmezse hata fırlatır.
                _list.Add(createEmployee);
               
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message); // Hatanın gösterildiği yer
            }
            return Ok();

        }

        // Employee güncelleyen Action Metot

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id,[FromBody] Employee updatedEmployee) // Değerleri Body'den alır.
        {
            var employee = _list.SingleOrDefault(x => x.Id == id);
            if(employee is null) // eğer employee boş ise böyle bir kayıtın olmadığı kullanıcıya döndürülür.
            {
                return BadRequest("Müşteri Bulunamadı");
            }
            // Fluent Validation çağırdığımız yer
            try
            {
                EmployeeValidator validator = new EmployeeValidator(); //Employeevalidator nesnesi oluşturulur.
                validator.ValidateAndThrow(updatedEmployee); // validasyon gerçekleşirse ekler gerçekleşmezse hata fırlatır.

                // Güncelleme işleminin yapıldığı yer

                employee.Name = updatedEmployee.Name != default ? updatedEmployee.Name : employee.Name;
                employee.LastName = updatedEmployee.LastName != default ? updatedEmployee.LastName : employee.LastName;
                employee.DateOfBirth = updatedEmployee.DateOfBirth != default ? updatedEmployee.DateOfBirth : employee.DateOfBirth;
                employee.Email = updatedEmployee.Email != default ? updatedEmployee.Email : employee.Email;
                employee.PhoneNumber = updatedEmployee.PhoneNumber != default ? updatedEmployee.PhoneNumber : employee.PhoneNumber;
                employee.Salary = updatedEmployee.Salary != default ? updatedEmployee.Salary : employee.Salary;
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message); // Hatanın döndürüldüğü yer.
            }

            return Ok();
        }
        // Employee silen Action Metot
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id) // Route'dan parametre alır
        {
            var employee = _list.SingleOrDefault(x => x.Id == id);
            if(employee is null) // employee boş ise kayıt bulunamadı döner
            {
                return BadRequest("Silinecek kayıt bulunamadı");
            }

            // kayıtı siler

            _list.Remove(employee);
            return Ok();
        }
    }
}
