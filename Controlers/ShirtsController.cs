 using Microsoft.AspNetCore.Mvc;
using minimalAPI.Filters;
using minimalAPI.Filters.ActionFilters;
using minimalAPI.Filters.ExceptionFilters;
using minimalAPI.Data;
using minimalAPI.Models;
using minimalAPI.Models.Repositories;


namespace minimalAPI.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    
    public class ShirtsController : ControllerBase
    {
        private readonly ApplicationDbContext db;
         public ShirtsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        
        public IActionResult GetShirts()
        {
            return Ok(db.Shirts.ToList());
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
        public IActionResult GetShirtById(int id)
        {
            
            return Ok(ShirtRepository.GetShirtById(id));
        }

        [HttpPost]    
        [Shirt_ValidateCreateShirtFilter]   
        public IActionResult CreateShirt([FromBody] Shirt shirt)
        {

            ShirtRepository.AddShirt(shirt);
            return CreatedAtAction(nameof(GetShirtById), new { id = shirt.ShirtId }, shirt);
        }

        [HttpPut]
        [Shirt_ValidateUpdateShirtFilter]
        [Shirt_ValidateShirtIdFilter]
        [Shirt_HandleUpdateExceptionsFilter]
        public IActionResult UpdateShirt(int id, Shirt shirt)
        {
            
            ShirtRepository.UpdateShirt(shirt);
            
            
            return NoContent();
        }
        [HttpDelete("{id}")]
        [Shirt_ValidateShirtIdFilter]
        public IActionResult DeleteShirt(int id)
        {
             var shirt = ShirtRepository.GetShirtById(id);
             ShirtRepository.DeleteShirt(id);
            
            return Ok($"Deleting shirt with {id}");
        }
    }
}