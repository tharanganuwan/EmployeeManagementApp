using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.services.Interface
{
    public interface IEmployeeService
    {
        public List<Employee> GetAllEmployee();
        public Employee GetSingleEmployee(long id);
        public void AddEmployee(Employee employee);
        public void DeleteEmployee(long id);
        public void UpdateEmployee(Employee employee);
    }
}
