using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceLayer.services.Interface;
using System.Security.Cryptography;
using DomainLayer.Dtos;
using DomainLayer.Models;
using Microsoft.Extensions.Configuration;
using EmployeeManagementApp.Helpers;

namespace EmployeeManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResetPasswordController : ControllerBase
    {
        private readonly IEmailService _service;
        private readonly IUserService _serviceUser;
        private readonly IConfiguration _config;

        public ResetPasswordController(IEmailService service, IUserService serviceUser, IConfiguration config)
        {
            _service = service;
            _serviceUser = serviceUser;
            _config = config;
        }

        [HttpPost("send=reset-email/{email}")]
        public async Task<IActionResult> SenndEmail(string email)
        {
            try
            {
                var userIsExits = await _serviceUser.UserEmailExist(email);
                if (!userIsExits)
                {
                    return NotFound(new
                    {
                        StatusCodes = 404,
                        Message = "email doesn't exist"
                    });
                }
                UserDto user = new UserDto();
                user.email = email;
                User userModel = await _serviceUser.Login(user);
                byte[] tokenBytes = new byte[64];
                RandomNumberGenerator.Fill(tokenBytes);
                var emailToken = Convert.ToBase64String(tokenBytes);
                userModel.resetPasswordToken = emailToken;
                userModel.resetPasswordExpiry = DateTime.Now.AddMinutes(15);
                await _serviceUser.updateResetPasswordToken(userModel);
                string form = _config["EmailSettings:From"];
                var emailModel = new EmailModel(email, "Reset Password!", EmailBody.EmailStringBody(email, emailToken));

                _service.SendEmail(emailModel);
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Email sent!"
                }
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while reset password: {ex.Message}");
            }
            

        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            try
            {
                UserDto userdto = new UserDto();
                userdto.email = resetPasswordDto.Email;
                var newToken = resetPasswordDto.EmailToken.Replace(" ", "+");
                User userModel = await _serviceUser.Login(userdto); //user ckeck to usse login api
                if (userModel is null)
                {
                    return NotFound(new
                    {
                        StatusCodes = 404,
                        Message = "email doesn't exist"
                    });
                }
                var tokenCode = userModel.resetPasswordToken;
                DateTime emailTokenExpiry = userModel.resetPasswordExpiry;
                if (tokenCode != resetPasswordDto.EmailToken || emailTokenExpiry < DateTime.Now)
                {
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        Message = "Imvalid Reset Link!"
                    });
                }
                //check password strength
                var pass = CheckPassword.ChechPasswordStrength(resetPasswordDto.NewPassword);
                if (!string.IsNullOrEmpty(pass))
                {
                    return BadRequest(new
                    {
                        StatusCode = 400,
                        Message = pass.ToString()
                    });
                }
                userModel.password = PasswordHasher.HashPassword(resetPasswordDto.NewPassword);
                await _serviceUser.updateResetPasswordToken(userModel);

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "password reset Sssessful!"
                }
                    );
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while reset password: {ex.Message}");
            }
            
        }
    }
}
