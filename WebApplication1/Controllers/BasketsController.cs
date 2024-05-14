using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Reposatory;

namespace Talabat.APIs.Controllers
{

    public class BasketsController : APIBaseController
    {
        private readonly IBasketRepo _basketRepo;

        public BasketsController(IBasketRepo basketRepo)
        {
            _basketRepo = basketRepo;
        }

        // End Points For GetBasket And if Don't Found a Basket ReCreate This Basket With Same IdBasked
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketAsync(string BasketId)
        {
            var Basket = await _basketRepo.GetBasketAsync(BasketId);

            return Basket is null ? new CustomerBasket(BasketId) : Basket;
        }

        // Update Or Create

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasketAsync(CustomerBasket customerBasket)
        {
            var Basket = await _basketRepo.UpdateBasketAsync(customerBasket);

            if (Basket is null) return BadRequest(new ApiResponse(400));
            return Ok(Basket);
        }

        //For Delete 
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasketAsync(string BasketId)
        {
            return await _basketRepo.DeleteBasketAsync(BasketId);
        }
    }
}
