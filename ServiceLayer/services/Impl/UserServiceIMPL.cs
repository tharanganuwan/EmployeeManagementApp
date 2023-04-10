using DomainLayer.Dtos;
using DomainLayer.Models;
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
    public class UserServiceIMPL : IUserService
    {
        private readonly AppDbContext _dbContext;

        public UserServiceIMPL(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User Login(UserDto user)
        {
            var founduser = _dbContext.tbl_user.FirstOrDefault(u => u.email == user.email);
            return founduser;
        }

        public void Register(User user)
        {
            _dbContext.tbl_user.Add(user);
            _dbContext.SaveChanges();
           
        }
    }
}
