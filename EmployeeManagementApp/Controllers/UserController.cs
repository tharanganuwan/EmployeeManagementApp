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
        public async Task<IActionResult> Register(UserDto user)
        {
            try
            {
                //check user name
                bool userExists = await _service.UserNameExist(user.userName);
                if (userExists)
                {
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        Message = $"User '{user.userName}' already exists.",
                    });
                }

                //check email
                bool emailExists = await _service.UserEmailExist(user.email);
                if (emailExists)
                {
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        Message = $"Email '{user.email}' already exists."
                    });
                }

                //check password strength
                var pass = ChechPassword.ChechPasswordStrength(user.password);
                if(!string.IsNullOrEmpty(pass))
                {
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        Message = pass.ToString()
                    });
                }


                User registerUser = _mapper.Map<User>(user);
                registerUser.password = PasswordHasher.HashPassword(user.password);
                registerUser.token = "";
                await _service.Register(registerUser);
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
        public async Task<IActionResult> Login(UserDto user)
        {
            try
            {
                User getUser = await _service.Login(user);
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

                getUser.token = CreateJwtToken.CreateJWT(getUser);

                return Ok(
                    new{
                        StatusCode = 200,
                        Message = "Login successfully.",
                        Token=getUser.token
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
