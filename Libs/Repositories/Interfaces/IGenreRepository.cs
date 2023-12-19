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
    public interface IGenreRepository : IRepositoryBase<Genre>
    {
        void Insert(Genre data);
        void Update(Genre data);
        void Delete(Genre data);
        List<Genre> getAll();
        IEnumerable<Genre> GetAll(Expression<Func<User, bool>> filter = null, Func<IQueryable<Genre>, IOrderedQueryable<Genre>> oderBy = null, int skip = 0, int take = 0);
        Genre GetById(object id);
        void Save();
        List<Movie> getAllMovieByGenre(int idGenres);
        int CountGenre();
    }
}
