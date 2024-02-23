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
    public interface IMovieRepository : IRepositoryBase<Movie>
    {
        void Insert(Movie data);
        void Update(Movie data);
        void Delete(Movie data);
        List<Movie> getAll();
        IEnumerable<Movie> GetAll(Expression<Func<Movie, bool>> filter = null, Func<IQueryable<Movie>, IOrderedQueryable<Movie>> oderBy = null, int skip = 0, int take = 0);
        Movie GetById(object id);
        void Save();
        List<Movie> GetTop6MovieView();
        int CountMovie();
        OddMovie GetURLOddMovie(int id);
        List<SeriesMovie> GetURLSeriesMovies(int id);
        string Top1Movie();

        int CountMovieOdd();

        int CountMovieSeries();
        List<Movie> HistoryMovieByUser(string userId);
        List<int> GetGenreByMovieId(int movieId);
        List<int> GetActorByMovieId(int movieId);
    }
}
