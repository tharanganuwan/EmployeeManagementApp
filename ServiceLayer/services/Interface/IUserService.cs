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
        void Register(User user);
        User Login(UserDto user);
    }
}
