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

        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Movie>> Get()
        {
            return _movieRepository.getAll();
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
