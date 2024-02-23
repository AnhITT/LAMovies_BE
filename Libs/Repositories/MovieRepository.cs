using Libs.Contracts;
using Libs.Data;
using Libs.Models;
using Libs.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
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
            var movies = _dbContext.Movies.ToList();
            DisplayToMovie(movies);
            return movies;
        }


        public void Insert(Movie data)
        {
            _dbContext.Movies.Add(data);
            _dbContext.SaveChanges();
            AddMovieToGenre(data);
            AddMovieToActor(data);
            _dbContext.SaveChanges();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void Update(Movie data)
        {
            _dbContext.Movies.Update(data);
            UpdateMovieToGenre(data);
            UpdateMovieToActor(data);
            _dbContext.SaveChanges();

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
        public OddMovie GetURLOddMovie(int id)
        {
            var item = _dbContext.OddMovies.FirstOrDefault(m => m.IdMovie == id);
            return item;
        }

        public List<SeriesMovie> GetURLSeriesMovies(int id)
        {
            var item = _dbContext.SeriesMovies.Where(m => m.IdMovie == id).ToList();
            return item;
        }
        public string Top1Movie()
        {
            var topMovie = _dbContext.Movies.OrderByDescending(p => p.View).FirstOrDefault();
            return topMovie.Name;
        }
        public int CountMovieOdd()
        {
            return _dbContext.Movies.Count(m => m.Type == "oddMovies");
        }
        public int CountMovieSeries()
        {
            return _dbContext.Movies.Count(m => m.Type == "seriesMovies");
        }
        public void DisplayToMovie(List<Movie> list)
        {
            foreach (var movie in list)
            {
                var genres = (from genre in _dbContext.Genres
                              join table in _dbContext.MovieGenres
                              on genre.Id equals table.IdGenre
                              where table.IdMovie == movie.Id
                              select genre.Name
                              ).ToList();
                movie.GenreNames = genres;
            }
            foreach (var movie in list)
            {
                var actors = (from actor in _dbContext.Actors
                              join table in _dbContext.MovieActors
                              on actor.Id equals table.IdActor
                              where table.IdMovie == movie.Id
                              select actor.Name
                              ).ToList();
                movie.ActorNames = actors;
            }
        }
        public void AddMovieToGenre(Movie model)
        {
            foreach (int genreId in model.Genres)
            {
                var movieGenre = new MovieGenre
                {
                    IdMovie = model.Id,
                    IdGenre = genreId
                };
                _dbContext.MovieGenres.Add(movieGenre);
            }
        }
        public void AddMovieToActor(Movie model)
        {
            foreach (int actorId in model.Actor)
            {
                var movieActor = new MovieActor
                {
                    IdMovie = model.Id,
                    IdActor = actorId
                };
                _dbContext.MovieActors.Add(movieActor);
            }
        }
        public void UpdateMovieToGenre(Movie model)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
            };

            var movieGenresToDelete = _dbContext.MovieGenres.Where(mg => mg.IdMovie == model.Id).ToList();

            foreach (var movieGenreToDelete in movieGenresToDelete)
            {
                _dbContext.MovieGenres.Remove(movieGenreToDelete);
            }

            foreach (int genreId in model.Genres)
            {
                var movieGenre = new MovieGenre
                {
                    IdMovie = model.Id,
                    IdGenre = genreId
                };
                _dbContext.MovieGenres.Add(movieGenre);
            }

        }

        public void UpdateMovieToActor(Movie model)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
            };

            var movieActorToDeletes = _dbContext.MovieActors.Where(ma => ma.IdMovie == model.Id).ToList();

            foreach (var movieActorToDelete in movieActorToDeletes)
            {
                _dbContext.MovieActors.Remove(movieActorToDelete);
            }
            foreach (int actorId in model.Actor)
            {
                var movieActor = new MovieActor
                {
                    IdMovie = model.Id,
                    IdActor = actorId
                };
                _dbContext.MovieActors.Add(movieActor);
            }
        }

        public List<Movie> HistoryMovieByUser(string userId)
        {
            List<MovieHistory> movieHistories = _dbContext.MovieHistorys.Where(up => up.IdUser == userId).ToList();
            var movies = from h in movieHistories
                         join m in _dbContext.Movies on h.IdMovie equals m.Id
                         orderby h.DateTimeWatch descending
                         select m;
            return movies.ToList();
        }
        public List<int> GetGenreByMovieId(int movieId)
        {
            var genreIds = _dbContext.MovieGenres.Where(a => a.IdMovie == movieId).Select(a => a.IdGenre).ToList();
            return genreIds;
        }
        public List<int> GetActorByMovieId(int movieId)
        {
            var genreIds = _dbContext.MovieActors.Where(a => a.IdMovie == movieId).Select(a => a.IdActor).ToList();
            return genreIds;
        }

    }
}
