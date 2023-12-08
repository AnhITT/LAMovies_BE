using Libs.Contracts;
using Libs.Data;
using Libs.Dtos;
using Libs.Models;
using Libs.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Repositories
{
    public class AccountRepository : RepositoryBase<User>, IAccountRepository
    {
        public AccountRepository(ApplicationDbContext dBContext) : base(dBContext) { }

        public void InsertAccount(User user)
        {
            _dbContext.Users.Add(user);
        }
        public bool CheckStatusAccount(User user)
        {
            if (user.Status)
            {
                return true;
            }
            return false;
        }
        public void UpdateAccount(User user)
        {
            _dbContext.Users.Update(user);
        }
        public void DeleteAccount(User user)
        {
            _dbContext.Users.Remove(user);
        }
        public List<User> getAll()
        {
            return _dbContext.Users.ToList();
        }
      
        public IEnumerable<User> GetPagedUsers(int page, int pageSize)
        {
            return GetList(orderBy: q => q.OrderBy(user => user.Id), skip: (page - 1) * pageSize, take: pageSize);
        }
        public User GetById(object id)
        {
            User user = _dbContext.Users.Find(id);
            if (user != null)
                return user;
            throw new Exception("UserID not found in Room!");
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public int CountAccount()
        {
            return _dbContext.Users.Count();
        }
        public void HanldeChange(User existingUser)
        {
            _dbContext.Entry(existingUser).State = EntityState.Detached;
        }
       
    }
}

