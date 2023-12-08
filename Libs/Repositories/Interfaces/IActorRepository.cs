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
    public interface IActorRepository : IRepositoryBase<Actor>
    {
        void InsertActor(Actor user);
        void UpdateActor(Actor user);
        void DeleteActor(Actor user);
        List<Actor> getAll();
        IEnumerable<Actor> GetAll(Expression<Func<User, bool>> filter = null, Func<IQueryable<Actor>, IOrderedQueryable<Actor>> oderBy = null, int skip = 0, int take = 0);
        Actor GetById(object id);
        void Save();
        List<Movie> getAllMovieByActor(int idActor);
        int CountActor();
    }
}
