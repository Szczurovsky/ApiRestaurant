using ApiRestaurant.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestaurant.Controllers
{
    [Route("api/restaurant")]
    public class RestaurantController:ControllerBase
    {
        private readonly RestaurantDbContext _dbContext;
        public RestaurantController(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ActionResult<IEnumerable<Restaurant>> GetAll()
        {
            var restaurants = _dbContext.Restaurants.ToList();
            return restaurants;
        }
    }
}
