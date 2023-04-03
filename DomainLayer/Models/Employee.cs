using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Employee
    {
        public long employeeId { get; set; }
        public string firstName { get; set; }
        public string middleName{ get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string company { get; set; }
        public EmployeeGender gender { get; set; }
        public double salary { get; set; }
        public int status { get; set; } //1= active 0=not active

    }
}
