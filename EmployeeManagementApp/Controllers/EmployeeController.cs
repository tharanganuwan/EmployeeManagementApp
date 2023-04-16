using AutoMapper;
using DomainLayer.Dtos;
using DomainLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceLayer.services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAngularLocalhost")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeService service, IMapper mapper, ILogger<EmployeeController> logger)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddEmployee(CreateEmployeeDto employee)
        {
            try
            {
                
                _service.AddEmployee(_mapper.Map<Employee>(employee));
                _logger.LogInformation($"Add employee : {employee.firstName} {DateTime.Now.ToString("h:mm tt")}");
                //return StatusCode(200, $"Successfull Adding new Employee");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"An error occurred while Adding new Employee: {DateTime.Now.ToString("h:mm tt")}");
                return StatusCode(500, $"An error occurred while Adding new Employee: {ex.Message}");
            }
        }

       
        [HttpGet]
        public ActionResult<ICollection<EmployeeDto>> GetAllEmployee() 
        {
            try
            {
                List<Employee> employees = _service.GetAllEmployee();
                if (employees is null)
                {
                    _logger.LogInformation($"Not Found employes {DateTime.Now.ToString("h:mm tt")}");
                    return NotFound();
                }
                _logger.LogInformation($"successfull get all Employees  {DateTime.Now.ToString("h:mm tt")}");
                return Ok(_mapper.Map<ICollection<EmployeeDto>>(employees));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"An error occurred while Getting Employees: {DateTime.Now.ToString("h:mm tt")}");
                return StatusCode(500, $"An error occurred while Getting Employees: {ex.Message}");
            }
        }

        
        [HttpGet("{employeeId}")]
        public ActionResult<EmployeeDto> GetSingleEmployee(long employeeId)
        {
            try
            {
                Employee employee = _service.GetSingleEmployee(employeeId);
                if (employee is null)
                {
                    _logger.LogInformation($"Not Found employee : {employeeId} {DateTime.Now.ToString("h:mm tt")}");
                    return NotFound();
                }
                _logger.LogInformation($"successfull get Employee : {employee.employeeId} {DateTime.Now.ToString("h:mm tt")}");
                return Ok(_mapper.Map<EmployeeDto>(employee));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"An error occurred while Getting Employees: {DateTime.Now.ToString("h:mm tt")}");
                return StatusCode(500, $"An error occurred while Getting Employees: {ex.Message}");
            }
        }

        [HttpDelete("{employeeId}")]
        public IActionResult DeleteEmployee(long employeeId)
        {
            try
            {
                Employee employee = _service.GetSingleEmployee(employeeId);
                if(employee is null) 
                {
                    _logger.LogInformation($"Not Found employee : employeeId {DateTime.Now.ToString("h:mm tt")}");
                    return NotFound();
                }
                _service.DeleteEmployee(employeeId);
                _logger.LogInformation($"successfull Delete Employee : {employee.employeeId} {DateTime.Now.ToString("h:mm tt")}");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"An error occurred while Deleteing Employees: {DateTime.Now.ToString("h:mm tt")}");
                return StatusCode(500, $"An error occurred while Deleteing Employees: {ex.Message}");
            }
        }

        [HttpPut("{employeeId}")]
        public IActionResult UpdateEmployee(long employeeId, CreateEmployeeDto newEmployeeDto)
        {
            try
            {
                Employee employee = _service.GetSingleEmployee(employeeId);
                if (employee is null)
                {
                    _logger.LogInformation($"Not Found Update employee : {employeeId} {DateTime.Now.ToString("h:mm tt")}");
                    return NotFound();
                }
                employee = _mapper.Map<Employee>(newEmployeeDto);
                employee.employeeId = employeeId;
                _service.UpdateEmployee(employee);
                _logger.LogInformation($"Update employee : {employee.employeeId} {DateTime.Now.ToString("h:mm tt")}");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"An error occurred while Updating Employee: {DateTime.Now.ToString("h:mm tt")}");
                return StatusCode(500, $"An error occurred while Updating Employee : {ex.Message}");
            }
        }
    }
}
