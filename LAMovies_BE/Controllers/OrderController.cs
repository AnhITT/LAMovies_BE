using Libs.Models;
using Libs.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LAMovies_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;
        private readonly IAccountRepository accountRepository;
        private readonly IPricingRepository pricingRepository;

        public OrderController(IOrderRepository orderRepository, IAccountRepository accountRepository, IPricingRepository pricingRepository)
        {
            this.orderRepository = orderRepository;
            this.accountRepository = accountRepository;
            this.pricingRepository = pricingRepository;
        }

        [HttpPost]
        [Route("CreateOrder")]
        public ActionResult CreateOrder(string idUser, int idPricing)
        {
            try
            {
                var user = accountRepository.GetById(idUser);
                var pricing = pricingRepository.GetById(idPricing);
                orderRepository.CreateOrder(user, pricing);
                return Ok("Success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
