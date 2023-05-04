using ApiRestaurant.Entities;
using ApiRestaurant.Exceptions;
using ApiRestaurant.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiRestaurant.Services
{
    public interface IRestaurantService
    {
        RestaurantDto GetById(int id);
        IEnumerable<RestaurantDto> GetAll();
        int CreateRestaurant(CreateRestaurantDto dto);
        void DeleteRestaurant(int id);
        void ModifyRestaurant(int id, ModifyRestaurantDto dto);
    }
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;
        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }
        public RestaurantDto GetById(int id)
        {
            var restaurant = _dbContext.Restaurants
                .Include(r => r.Dishes)
                .Include(r => r.Address)
                .FirstOrDefault(x => x.Id == id);
        
            if(restaurant is null) throw new NotFoundException("Restaurant not found"); ;

            var result = _mapper.Map<RestaurantDto>(restaurant);
            return result;
        }
        public IEnumerable<RestaurantDto> GetAll()
        {
            var restaurants = _dbContext.Restaurants
                .Include(r => r.Dishes)
                .Include(r => r.Address)
                .ToList();
            var restaurantsDtos = _mapper.Map<List<RestaurantDto>>(restaurants);
            return restaurantsDtos;
        }
        public int CreateRestaurant(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();
            return restaurant.Id;
        }
        public void DeleteRestaurant(int id)
        {
            _logger.LogError($"Restaurant with id: {id} delete action invoked");
            var restaurant = _dbContext.Restaurants
              .FirstOrDefault(x => x.Id == id);
            if (restaurant is null) throw new NotFoundException("Restaurant not found");
            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges();
          
        }
        public void ModifyRestaurant(int id, ModifyRestaurantDto dto)
        {
            var restaurant = _dbContext.Restaurants
                .FirstOrDefault(x => x.Id==id);
            if(restaurant is null)  throw new NotFoundException("Restaurant not found");    
            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.HasDelivery = dto.HasDelivery;
            _dbContext.SaveChanges();
        }


    }
}
