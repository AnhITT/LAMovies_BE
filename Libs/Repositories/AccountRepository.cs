using Libs.Contracts;
using Libs.Data;
using Libs.Models;
using Libs.Repositories.Interfaces;
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

        public IEnumerable<User> GetAll(Expression<Func<User, bool>> filter = null, Func<IQueryable<User>, IOrderedQueryable<User>> oderBy = null, int skip = 0, int take = 0)
        {
            throw new NotImplementedException();
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
    }
}

