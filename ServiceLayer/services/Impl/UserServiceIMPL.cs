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

        public async Task<User> Login(UserDto user)
        {
            var founduser = await _dbContext.tbl_user.FirstOrDefaultAsync(u => u.email == user.email);
            if (founduser == null)
            {
                return null;
            }
            return founduser;
        }

        public async Task Register(User user)
        {
            _dbContext.tbl_user.Add(user);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<bool> UserEmailExist(string email)
        {
            return await _dbContext.tbl_user.AnyAsync(x => x.email == email);
        }

        public async Task<bool> UserNameExist(string user_Name)
        {
            return await _dbContext.tbl_user.AnyAsync(x=> x.userName==user_Name);
        }
    }
}
