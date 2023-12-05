using Libs.Models;
using Libs.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LAMovies_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository genreRepository;

        public GenreController(IGenreRepository genreRepository)
        {
            this.genreRepository = genreRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Genre>> GetAll()
        {
            var data = genreRepository.getAll();
            return Ok(data);
        }
        [HttpGet]
        [Route("GetMovieByGenre")]
        public ActionResult<IEnumerable<Movie>> GetMovieByGenre(int id)
        {
            try
            {
                var data = genreRepository.getAllMovieByGenre(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetGenreById")]
        public ActionResult<Genre> GetGenreById(int id)
        {
            try
            {
                var data = genreRepository.GetById(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("CreateGenre")]
        public ActionResult<Genre> CreateGenre([FromBody] Genre data)
        {
            genreRepository.Insert(data);
            genreRepository.Save();

            return CreatedAtAction(nameof(GetGenreById), new { id = data.Id }, data);
        }

        [HttpPut]
        public ActionResult<Actor> UpdateGenre(int id, [FromBody] Genre genre)
        {
            try
            {
                var data = genreRepository.GetById(id);

                if (data == null)
                {
                    return NotFound("Genre not found");
                }

                data.Name = genre.Name; // Update other properties as needed
                genreRepository.Update(data);
                genreRepository.Save();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult DeleteGenre(int id)
        {
            try
            {
                var data = genreRepository.GetById(id);

                if (data == null)
                {
                    return NotFound("Genre not found");
                }

                genreRepository.Delete(data);
                genreRepository.Save();

                return Ok("Delete Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
