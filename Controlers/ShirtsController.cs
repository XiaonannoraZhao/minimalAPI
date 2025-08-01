 using Microsoft.AspNetCore.Mvc;
using minimalAPI.Filters;
using minimalAPI.Filters.ActionFilters;

using minimalAPI.Models;
using minimalAPI.Models.Repositories;


namespace minimalAPI.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    
    public class ShirtsController : ControllerBase
    {
        [HttpGet]
        
        public IActionResult GetShirts()
        {
            Console.WriteLine("V1: GetShirts called");
            return Ok(ShirtRepository.GetShirts());
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(Shirt_ValidateShirtIdFilterAttribute))]
        public IActionResult GetShirtById(int id)
        {
            
            return Ok(ShirtRepository.GetShirtById(id));
        }

        [HttpPost]       
        public IActionResult CreateShirt([FromBody]Shirt shirt)
        {
            if (shirt == null)
            {
                return BadRequest("Shirt cannot be null.");
            }
            var existingShirt = ShirtRepository.GetShirtByProperties(shirt.Brand, shirt.Gender, shirt.Color, shirt.Size);
            if (existingShirt != null)
            
                return BadRequest("Shirt with the same properties already exists.");
            
            ShirtRepository.AddShirt(shirt);
            return CreatedAtAction(nameof(GetShirtById), new { id = shirt.ShirtId }, shirt);
        }       

        [HttpPut]
        
        public IActionResult UpdateShirt(int id)
        {
            return Ok($"Updating shirt with {id}"); 
        }
        [HttpDelete]
        
        public IActionResult DeleteShirt(int id)
        {
            return Ok($"Deleting shirt with {id}") ;
        }
    }
}