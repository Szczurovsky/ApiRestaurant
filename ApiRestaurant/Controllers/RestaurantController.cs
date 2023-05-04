using ApiRestaurant.Entities;
using ApiRestaurant.Models;
using ApiRestaurant.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiRestaurant.Controllers
{
    [Route("api/restaurant")]
    public class RestaurantController:ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }
        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurantsDtos = _restaurantService.GetAll();
            return Ok(restaurantsDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> Get([FromRoute] int id)
        {
            var restaurant = _restaurantService.GetById(id);

            return Ok(restaurant);
        }
        [HttpPost]
        public ActionResult CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var restaurantId = _restaurantService.CreateRestaurant(dto);

            return Created($"/api/restaurant/{restaurantId}", null);
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteRestaurant([FromRoute]int id)
        {
            _restaurantService.DeleteRestaurant(id);
            return NoContent();

        }
        [HttpPut("{id}")]
        public ActionResult ModifyRestaurant([FromRoute]int id, [FromBody] ModifyRestaurantDto dto)
        {
            _restaurantService.ModifyRestaurant(id, dto);
            return Ok();

        }
    }
}
