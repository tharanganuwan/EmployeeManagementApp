using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Dtos
{
    public class EmployeeDto
    {
        public long employeeId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string company { get; set; }
        public EmployeeGender gender { get; set; }
        public double salary { get; set; }
    }
}
