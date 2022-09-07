using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PayCore_HW2.Extensions
{
    public static class EmployeeExtension
    {
        // Employee sınıfının DateofBirth property'si için yazılmış extension metot.
        public static bool DateOfBirth(DateTime time)
        {

            
            var max = new DateTime(2002, 10, 10);
            var min = new DateTime(1945, 11, 11);
            if (time < min || time > max)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        // Employee sınıfının Email property'si için yazılmış extension metot.
        public static bool Email(string email)
        {
            // Regex ifadesinde özel karakterleri içermeyecek şekilde eşlenme sağlanmıştır.
            // '-'  '_'  '@' ve '.' ifadelerini kabul eder
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            // Email içerisinde rakam var mı yok mu kontrolü yapılıp result değerine boolean tipte atanır.
            var result = email.Any(x => char.IsDigit(x));
            if (result)
            {
                // Eğer rakam var ise false geri dönüş verir.
                return false;
            }

            if (regex.IsMatch(email))
            {
                // Regex ifadesi ile eşleşir ise true geri dönüş verir.
                return true;
            }
            return false;

        }
        public static bool PhoneNumber(string pnumber)
        {
            // Uluslararası telefon numarası regex ifadesidir.
            // Alan kodu zorunludur (+) işareti ile başlamalıdır.
            Regex regex = new Regex(@"^\+(?:[0-9]){6,14}[0-9]$");
            if (regex.IsMatch(pnumber)) // Parametre olarak gelen pnumber regex ile eşleşirse true geri dönüş verir.
            {
                return true;
            }
            return false;

        }
    }
}
