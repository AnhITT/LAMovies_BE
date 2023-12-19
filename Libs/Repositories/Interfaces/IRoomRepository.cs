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
    public interface IRoomRepository : IRepositoryBase<Room>
    {
        void Insert(Room data);
        void Update(Room data);
        void Delete(Room data);
        List<Room> getAll();
        IEnumerable<Room> GetAll(Expression<Func<User, bool>> filter = null, Func<IQueryable<Room>, IOrderedQueryable<Room>> oderBy = null, int skip = 0, int take = 0);
        Room GetById(object id);
        void Save();
    }
}
