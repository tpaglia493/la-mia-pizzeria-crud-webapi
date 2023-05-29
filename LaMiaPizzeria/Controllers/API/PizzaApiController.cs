using LaMiaPizzeria.DataBase;
using LaMiaPizzeria.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaMiaPizzeria.Controllers.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzaApiController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetPizzas()
        {
            using (PizzaContext db = new())
            {
                List<PizzaModel> pizzaModels = db.Pizze.ToList();
                return Ok(pizzaModels);
            }

        }
    }
}
