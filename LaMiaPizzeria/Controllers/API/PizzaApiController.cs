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


        //TO TEST
        [HttpPost]
        public IActionResult PostPizza([FromBody] PizzaModel pizza)
        {
            using (PizzaContext db = new())
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
                {
                    db.Pizze.Add(pizza);
                    db.SaveChanges();
                    return Ok();
                }
            }
        }
    }
}
