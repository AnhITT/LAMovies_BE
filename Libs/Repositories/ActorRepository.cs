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
    public class ActorRepository : RepositoryBase<Actor>, IActorRepository
    {
        public ActorRepository(ApplicationDbContext dBContext) : base(dBContext) { }

        public void DeleteActor(Actor user)
        {
            _dbContext.Remove(user);
        }

        public List<Actor> getAll()
        {
            return _dbContext.Actors.ToList();
        }

        public IEnumerable<Actor> GetAll(Expression<Func<User, bool>> filter = null, Func<IQueryable<Actor>, IOrderedQueryable<Actor>> oderBy = null, int skip = 0, int take = 0)
        {
            throw new NotImplementedException();
        }

        public void InsertActor(Actor user)
        {
            _dbContext.Actors.Add(user);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateActor(Actor user)
        {
            _dbContext.Actors.Update(user);
        }
        public Actor GetById(object id)
        {
            Actor user = _dbContext.Actors.Find(id);
            if (user != null)
                return user;
            throw new Exception("ActorID not found in Room!");
        }
        public List<Movie> getAllMovieByActor(int idActor)
        {
            return _dbContext.MovieActors.Where(g => g.IdActor == idActor).Select(m => m.Movie).ToList();
        }
        public int CountActor()
        {
            return _dbContext.Actors.Count();
        }
    }
}
