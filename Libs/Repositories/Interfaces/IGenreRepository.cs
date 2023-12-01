using Libs.Contracts;
using Libs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Repositories.Interfaces
{
    public interface IGenreRepository : IRepositoryBase<Actor>
    {
        void Insert(Genre data);
        void Update(Genre data);
        void Delete(Genre data);
        List<Genre> getAll();
        IEnumerable<Actor> GetAll(Expression<Func<User, bool>> filter = null, Func<IQueryable<Actor>, IOrderedQueryable<Actor>> oderBy = null, int skip = 0, int take = 0);
        Genre GetById(object id);
        void Save();
    }
}
