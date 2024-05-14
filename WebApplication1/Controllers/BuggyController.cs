using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Reposatory.Data;

namespace Talabat.APIs.Controllers
{

    public class BuggyController : APIBaseController
    {
        private readonly StoreContext _dbContext;

        public BuggyController(StoreContext dbContext)
        {
            this._dbContext = dbContext;
        }


        [HttpGet("NotFound")]
        public ActionResult GetNotFoundError()
        {
            var Product = _dbContext.Products.Find(100);
            if (Product == null) 
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(Product);
        }

        [HttpGet("ServerError")]

        public ActionResult GetServerError() 
        {
            var Product = _dbContext.Products.Find(100);

           var ProductReturn = Product.ToString();

            return Ok(ProductReturn);

        }

        [HttpGet("BadRequest")]

        public ActionResult GetbadRequestError() 
        {
            return BadRequest();
        }

        [HttpGet("BadRequest/{id}")]
        public ActionResult GetBadRequest(int id) 
        {
            return Ok();
        }
    }
}
