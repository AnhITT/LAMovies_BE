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
    public class MovieRepository : RepositoryBase<Movie>, IMovieRepository
    {
        public MovieRepository(ApplicationDbContext dBContext) : base(dBContext) { }

        public void Delete(Movie data)
        {
            _dbContext.Remove(data);
        }

        public List<Movie> getAll()
        {
            return _dbContext.Movies.ToList();
        }


        public void Insert(Movie data)
        {
            _dbContext.Movies.Add(data);
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(Movie data)
        {
            _dbContext.Movies.Update(data);
        }
        public Movie GetById(object id)
        {
            Movie data = _dbContext.Movies.Find(id);
            if (data != null)
                return data;
            throw new Exception("Movie not found");
        }
        public List<Movie> GetTop6MovieView()
        {
            var topMovies = _dbContext.Movies.OrderByDescending(p => p.View).Take(8).ToList();
            return topMovies;
        }
        public IEnumerable<Movie> GetAll(Expression<Func<Movie, bool>> filter = null, Func<IQueryable<Movie>, IOrderedQueryable<Movie>> oderBy = null, int skip = 0, int take = 0)
        {
            throw new NotImplementedException();
        }
        public int CountMovie()
        {
            return _dbContext.Movies.Count();
        }
    }
}
