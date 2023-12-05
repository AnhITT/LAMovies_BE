using Libs.Models;
using Libs.Repositories;
using Libs.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LAMovies_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorRepository _actorRepository;

        public ActorController(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Actor>> GetAllActors()
        {
            var actors = _actorRepository.getAll();
            return Ok(actors);
        }
        [HttpGet]
        [Route("GetMovieByActor")]
        public ActionResult<IEnumerable<Movie>> GetMovieByActor(int id)
        {
            try
            {
                var data = _actorRepository.getAllMovieByActor(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetActorById")]
        public ActionResult<Actor> GetActorById(int id)
        {
            try
            {
                var actor = _actorRepository.GetById(id);
                return Ok(actor);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("CreateActor")]
        public ActionResult<Actor> CreateActor([FromBody] Actor actor)
        {
            _actorRepository.InsertActor(actor);
            _actorRepository.Save();

            return CreatedAtAction(nameof(GetActorById), new { id = actor.Id }, actor);
        }

        [HttpPut]
        public ActionResult<Actor> UpdateActor(int id, [FromBody] Actor actor)
        {
            try
            {
                var existingActor = _actorRepository.GetById(id);

                if (existingActor == null)
                {
                    return NotFound("Actor not found");
                }

                existingActor.Name = actor.Name;
                existingActor.Avarta = actor.Avarta;
                existingActor.Description = actor.Description;
                _actorRepository.UpdateActor(existingActor);
                _actorRepository.Save();

                return Ok(existingActor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult DeleteActor(int id)
        {
            try
            {
                var actor = _actorRepository.GetById(id);

                if (actor == null)
                {
                    return NotFound("Actor not found");
                }

                _actorRepository.DeleteActor(actor);
                _actorRepository.Save();

                return Ok("Delete Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}