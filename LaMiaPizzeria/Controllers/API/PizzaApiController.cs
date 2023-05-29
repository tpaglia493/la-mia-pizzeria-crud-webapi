using LaMiaPizzeria.DataBase;
using LaMiaPizzeria.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaMiaPizzeria.Controllers.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PizzaApiController : ControllerBase
    {
        //GET ALL PIZZAS FROM DB
        [HttpGet]
        public IActionResult GetPizzas()
        {
            using (PizzaContext db = new())
            {
                List<PizzaModel> pizzaModels = db.Pizze.ToList();
                return Ok(pizzaModels);
            }

        }

        //GET PIZZA BY ID (pass ID by URL)
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

        //GET PIZZAS BY KEYWORD (pass keyword by URL)
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


        //POST A NEW PIZZA IN DB
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

        //DELETE A PIZZA BY ID (pass ID by query parameters)
        [HttpDelete]
        public IActionResult DeletePizza(int id)
        {
            using (PizzaContext db = new())
            {
                PizzaModel? pizzaToDelete = db.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();
                if (pizzaToDelete != null)
                {
                    db.Pizze.Remove(pizzaToDelete);
                    db.SaveChanges();
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
        }

        //UPDATE A PIZZA BY ID (pass ID by query parameters)
        [HttpPut]
        public IActionResult UpdatePizza(int id, [FromBody] PizzaModel updatedPizza)
        {
            using (PizzaContext db = new())
            {
                PizzaModel? pizzaToUpdate = db.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();
                if (pizzaToUpdate == null)
                {
                    return NotFound();
                }
                else
                {
                    pizzaToUpdate.Name = updatedPizza.Name;
                    pizzaToUpdate.pizzaCategoryId = updatedPizza.pizzaCategoryId;


                }
            }
        }
    }
}
