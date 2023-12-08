using Libs.Dtos;
using Libs.Models;
using Libs.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LAMovies_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IActorRepository _actorRepository;
        private readonly IGenreRepository _genreRepository;


        public MovieController(IMovieRepository movieRepository, IAccountRepository accountRepository,
           IActorRepository actorRepository, IGenreRepository genreRepository)
        {
            _movieRepository = movieRepository;
            _accountRepository = accountRepository;
            _actorRepository = actorRepository;
             _genreRepository = genreRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Movie>> Get()
        {
            return _movieRepository.getAll();
        }
        [HttpGet]
        [Route("GetTop6MovieView")]
        public ActionResult<IEnumerable<Movie>> GetTop6MovieView()
        {
            try
            {
                var movie = _movieRepository.GetTop6MovieView();
                return Ok(movie);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        [Route("ThongKe")]
        public ActionResult<ThongKeDTO> ThongKe()
        {
            try
            {
                var data = new ThongKeDTO();
                data.countMovies = _movieRepository.CountMovie();
                data.countAccount = _accountRepository.CountAccount();
                data.countActor = _actorRepository.CountActor();
                data.countGenre = _genreRepository.CountGenre();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        // GET: api/Movie/5
        [HttpGet]
        [Route("GetMovieById")]
        public ActionResult<Movie> GetMovieById(int id)
        {
            try
            {
                var movie = _movieRepository.GetById(id);
                return Ok(movie);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("CreateMovie")]
        public ActionResult CreateMovie([FromBody] Movie movie)
        {
            try
            {
                _movieRepository.Insert(movie);
                _movieRepository.Save();
                return Ok("Create Movie Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public ActionResult UpdateMovie(int id, [FromBody] Movie movie)
        {
            try
            {
                var existingMovie = _movieRepository.GetById(id);
                if (existingMovie != null)
                {
                    existingMovie = movie;
                    _movieRepository.Update(existingMovie);
                    _movieRepository.Save();

                    return Ok(existingMovie);
                }
                else
                {
                    return NotFound("Movie not found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            try
            {
                var movie = _movieRepository.GetById(id);
                if (movie != null)
                {
                    _movieRepository.Delete(movie);
                    _movieRepository.Save();
                    return Ok("Delete Movie Success");
                }
                else
                {
                    return NotFound("Movie not found");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
