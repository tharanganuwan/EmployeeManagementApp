using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Employee
    {
        public long employeeId { get; set; }
        [MaxLength(100)]
        public string firstName { get; set; }
        [MaxLength(100)]
        public string middleName{ get; set; }
        [MaxLength(100)]
        public string lastName { get; set; }
        [MaxLength(100)]
        public string email { get; set; }
        public DateTime dateOfBirth { get; set; }
        [MaxLength(100)]
        public string company { get; set; }
        public EmployeeGender gender { get; set; }
        [MaxLength(50)]
        public double salary { get; set; }

        public int status { get; set; } //1= active 0=not active

    }
}
