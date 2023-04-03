using DomainLayer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.DbContextLayer;
using ServiceLayer.services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.services.Impl
{
    public class EmployeeServiceIMPL : IEmployeeService
    {
        private readonly AppDbContext _dbContext;

        public EmployeeServiceIMPL(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddEmployee(Employee employee)
        {
            employee.status = 1;
            _dbContext.Database.ExecuteSqlRaw(
                "EXEC CreateEmployee @EmployeeId, @FirstName, @MiddleName, @LastName, @Email, @DateOfBirth, @Company, @Gender, @Salary, @Status",
                new SqlParameter("@EmployeeId", employee.employeeId),
                new SqlParameter("@FirstName", employee.firstName),
                new SqlParameter("@MiddleName", employee.middleName),
                new SqlParameter("@LastName", employee.lastName),
                new SqlParameter("@Email", employee.email),
                new SqlParameter("@DateOfBirth", employee.dateOfBirth),
                new SqlParameter("@Company", employee.company),
                new SqlParameter("@Gender", employee.gender),
                new SqlParameter("@Salary", employee.salary),
                new SqlParameter("@Status", employee.status)
            );
        }

        public void DeleteEmployee(long id)
        {
            _dbContext.Database.ExecuteSqlRaw("EXEC DeleteEmployee @EmployeeId",
                new SqlParameter("@EmployeeId", id)
            );
        }

        public List<Employee> GetAllEmployee()
        {
            return _dbContext.tbl_employee.FromSqlRaw("GetEmployee").ToList();
        }

        public Employee GetSingleEmployee(long id)
        {
            return _dbContext.tbl_employee.FromSqlRaw("GetEmployee @EmployeeId", new SqlParameter("@EmployeeId", id)).AsEnumerable().FirstOrDefault();
        }

        public void UpdateEmployee(Employee employee)
        {
            _dbContext.Database.ExecuteSqlRaw(
                "EXEC UpdateEmployee @EmployeeId, @FirstName, @MiddleName, @LastName, @Email, @DateOfBirth, @Company, @Gender, @Salary, @Status",
                new SqlParameter("@EmployeeId", employee.employeeId),
                new SqlParameter("@FirstName", employee.firstName),
                new SqlParameter("@MiddleName", employee.middleName),
                new SqlParameter("@LastName", employee.lastName),
                new SqlParameter("@Email", employee.email),
                new SqlParameter("@DateOfBirth", employee.dateOfBirth),
                new SqlParameter("@Company", employee.company),
                new SqlParameter("@Gender", employee.gender),
                new SqlParameter("@Salary", employee.salary),
                new SqlParameter("@Status", employee.status)
            );
        }
    }
}
