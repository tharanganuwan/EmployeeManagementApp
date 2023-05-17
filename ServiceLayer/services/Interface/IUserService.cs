using DomainLayer.Dtos;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.services.Interface
{
    public interface IUserService
    {
        Task Register(User user);
        Task<User> Login(UserDto user);
        Task<bool> UserNameExist(string user_Name);
        Task<bool> UserEmailExist(string email);
        Task updateResetPasswordToken(User user);
    }
}
