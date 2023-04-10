using AutoMapper;
using DomainLayer.Dtos;
using DomainLayer.Models;
using EmployeeManagementApp.Helpers;
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
    [EnableCors("AllowAngularLocalhost")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        public UserController(IUserService service, ILogger<UserController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;

        }

        [HttpPost("register")]
        public IActionResult Register(UserDto user)
        {
            try
            {
                User rUser = _mapper.Map<User>(user);
                rUser.password = PasswordHasher.HashPassword(user.password);
                rUser.token = "";
                _service.Register(rUser);
                return Ok(
                    new
                    {
                        StatusCode = 200,
                        Message = "User Regisreation successfully."
                    });
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"An error occurred while Regidtering new User: {DateTime.Now.ToString("h:mm tt")}");
                return StatusCode(500, $"An error occurred while Regidtering new User: {ex.Message}");
            }

        }

        [HttpPost("login")]
        public IActionResult Login(UserDto user)
        {
            try
            {
                User getUser = _service.Login(user);
                if (getUser == null)
                {
                    return BadRequest(new { 
                        StatusCode = 404,
                        Message = "User Not Found."
                    });
                }
                if (!PasswordHasher.VerifyPassword(user.password, getUser.password))
                {
                    return BadRequest(new
                    {
                        StatusCode = 404,
                        Message = "password is Incorrect."
                    });
                }
                return Ok(
                    new{
                        StatusCode = 200,
                        Message = "Login successfully."
                    }
                );
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"An error occurred while Regidtering new User: {DateTime.Now.ToString("h:mm tt")}");
                return StatusCode(500, $"An error occurred while Regidtering new User: {ex.Message}");
            }

        }
    }
}
