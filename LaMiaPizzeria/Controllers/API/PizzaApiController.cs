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

        [HttpGet("{id}")]
        public IActionResult GetPizzaById(int id)
        {
            using (PizzaContext db = new())
            {
                PizzaModel? pizzaToFind = db.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();
                if (pizzaToFind != null)
                {

                    return Ok(pizzaToFind);
                }
                else
                {
                    return NotFound();
                }
            }

        }
        //TO TEST

        [HttpGet("{keyWord}")]
        public IActionResult GetPizzasByKeyword(string keyWord)
        {
            using (PizzaContext db = new())
            {
                List<PizzaModel> pizzasByKeyword = db.Pizze.Where(pizza => pizza.Name.Contains(keyWord)).ToList();
                if (pizzasByKeyword != null)
                {
                    return Ok(pizzasByKeyword);
                }
                else
                {
                    return NotFound();
                }
            }
        }



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
