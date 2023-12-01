using Libs.Contracts;
using Libs.DTOs;
using Libs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Repositories.Interfaces
{
    public interface IAccountRepository : IRepositoryBase<User>
    {
        void InsertAccount(User user);
        void UpdateAccount(User user);
        void DeleteAccount(User user);
        List<User> getAll();
        IEnumerable<User> GetAll(Expression<Func<User, bool>> filter = null, Func<IQueryable<User>, IOrderedQueryable<User>> oderBy = null, int skip = 0, int take = 0);
        User GetById(object id);
        void Save();
    }
}
