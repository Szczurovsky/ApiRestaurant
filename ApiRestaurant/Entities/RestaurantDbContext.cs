﻿using Microsoft.EntityFrameworkCore;

namespace ApiRestaurant.Entities
{
    public class RestaurantDbContext :DbContext
    {
        private string _connectionString = "Server=(LocalDB)\\MSSQLLocalDB;Database=RestaurantDBApi;Trusted_Connection=True;";

        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Dish> Dishes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
                .Property(r => r.Name)
                .IsRequired();
            modelBuilder.Entity<Dish>()
                .Property(d => d.Name)
                .IsRequired();

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
