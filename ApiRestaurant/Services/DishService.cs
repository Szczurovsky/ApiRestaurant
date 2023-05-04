using ApiRestaurant.Entities;
using ApiRestaurant.Exceptions;
using ApiRestaurant.Models;
using AutoMapper;

namespace ApiRestaurant.Services
{
    public interface IDishService
    {
        int Create (int restaurantId, CreateDishDto dto);
    }
    public class DishService : IDishService
    {
        private readonly RestaurantDbContext _context;
        private readonly IMapper _mapper;

        public DishService(RestaurantDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Create(int restaurantId, CreateDishDto dto)
        {
            var restaurant = _context.Restaurants.FirstOrDefault(x => x.Id == restaurantId);
            if (restaurant is null)
                throw new NotFoundException("restaurant not found");

            var dishEntity = _mapper.Map<Dish>(dto);
            dishEntity.RestaurantId = restaurantId;
            _context.Dishes.Add(dishEntity);
            _context.SaveChanges();

            return dishEntity.Id;
        }
    }
}
