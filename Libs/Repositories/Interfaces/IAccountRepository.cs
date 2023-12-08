using Libs.Contracts;
using Libs.Dtos;
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
        IEnumerable<User> GetPagedUsers(int page, int pageSize);
        User GetById(object id);
        void Save();
        bool CheckStatusAccount(User user);
        int CountAccount();
        void HanldeChange(User existingUser);
    }
}
