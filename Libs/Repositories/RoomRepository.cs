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
    public class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(ApplicationDbContext dBContext) : base(dBContext) { }

        public void Delete(Room data)
        {
            _dbContext.Remove(data);
        }

        public List<Room> getAll()
        {
            return _dbContext.Rooms.ToList();
        }

        public void Insert(Room data)
        {
            _dbContext.Rooms.Add(data);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(Room data)
        {
            _dbContext.Rooms.Update(data);
        }
        public Room GetById(object id)
        {
            Room data = _dbContext.Rooms.Find(id);
            if (data != null)
                return data;
            throw new Exception("Room not found");
        }

        public IEnumerable<Room> GetAll(Expression<Func<User, bool>> filter = null, Func<IQueryable<Room>, IOrderedQueryable<Room>> oderBy = null, int skip = 0, int take = 0)
        {
            throw new NotImplementedException();
        }
    }
}
