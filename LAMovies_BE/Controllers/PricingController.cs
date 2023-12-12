using Libs.Dtos;
using Libs.Models;
using Libs.Repositories;
using Libs.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LAMovies_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingController : ControllerBase
    {
        private readonly IPricingRepository pricingRepository;
        private readonly IAccountRepository accountRepository;
        public PricingController(IPricingRepository pricingRepository, IAccountRepository accountRepository)
        {
            this.pricingRepository = pricingRepository;
            this.accountRepository = accountRepository;
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
        [HttpGet]
        [Route("CheckPricing")]
        public ActionResult CheckPricing(string id)
        {
            try
            {
                var user = accountRepository.GetById(id);
                var data = pricingRepository.CheckPricing(user);
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
        [HttpGet]
        [Route("GetUserPricing")]
        public IActionResult QLUserPricing()
        {
            try
            {
                var listUserPricing = pricingRepository.GetAllUserPricings().ToList();
                List<ListUserPricingDTO> list = new List<ListUserPricingDTO>();
                foreach (var item in listUserPricing)
                {
                    var user = accountRepository.GetById(item.IdUser);
                    var pricing = pricingRepository.GetById(item.IdPricing);
                    ListUserPricingDTO userPricingDTO = new ListUserPricingDTO()
                    {
                        UserNameUser = user.UserName,
                        FullNameUser = user.FullName,
                        NamePricing = pricing.Name,
                        StartTime = item.StartTime,
                        EndTime = item.EndTime,
                        RemainingTime = item.EndTime - DateTime.Now
                    };
                    list.Add(userPricingDTO);
                }
                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
        [HttpGet]
        [Route("GetInfoPricing")]
        public IActionResult GetInfoPricing(string id)
        {
            try
            {
                var user = accountRepository.GetById(id);
                var userPricing = pricingRepository.GetPricingByUser(user);
                if (userPricing != null)
                {
                    return Ok(userPricing);
                }
                else
                {
                    return BadRequest("Ko co");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
