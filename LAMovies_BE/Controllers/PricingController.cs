using Libs.Models;
using Libs.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LAMovies_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingController : ControllerBase
    {
        private readonly IPricingRepository pricingRepository;

        public PricingController(IPricingRepository pricingRepository)
        {
            this.pricingRepository = pricingRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Pricing>> GetAll()
        {
            var data = pricingRepository.getAll();
            return Ok(data);
        }

        [HttpGet]
        [Route("GetPricingById")]
        public ActionResult<Pricing> GetPricingById(int id)
        {
            try
            {
                var data = pricingRepository.GetById(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Route("CreatePricing")]
        public ActionResult<Pricing> CreatePricing([FromBody] Pricing data)
        {
            pricingRepository.Insert(data);
            pricingRepository.Save();

            return CreatedAtAction(nameof(GetPricingById), new { id = data.Id }, data);
        }

        [HttpPut]
        public ActionResult<Actor> UpdatePricing(int id, [FromBody] Pricing Pricing)
        {
            try
            {
                var data = pricingRepository.GetById(id);

                if (data == null)
                {
                    return NotFound("Pricing not found");
                }

                data = Pricing;
                pricingRepository.Update(data);
                pricingRepository.Save();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult DeletePricing(int id)
        {
            try
            {
                var data = pricingRepository.GetById(id);

                if (data == null)
                {
                    return NotFound("Pricing not found");
                }

                pricingRepository.Delete(data);
                pricingRepository.Save();

                return Ok("Delete Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
