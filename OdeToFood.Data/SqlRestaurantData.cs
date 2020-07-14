using System.Collections.Generic;
using OdeToFood.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext dbContext;

        public SqlRestaurantData(OdeToFoodDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Restaurant AddRestaurant(Restaurant restaurant)
        {
            dbContext.Restaurants.Add(restaurant);
            return restaurant;
        }

        public int Commit()
        {
            return dbContext.SaveChanges();
        }

        public Restaurant DeleteRestaurant(int id)
        {
            Restaurant restaurant = GetRestaurantById(id);
            if (restaurant != null)
            {
                dbContext.Restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public Restaurant GetRestaurantById(int id)
        {
            return dbContext.Restaurants.Find(id);
        }

        public IEnumerable<Restaurant> GetRestaurants()
        {
            return from restaurant in dbContext.Restaurants
                   orderby restaurant.Name
                   select restaurant;
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            return from r in dbContext.Restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }

        public Restaurant UpdateRestaurant(Restaurant restaurant)
        {
            var entity = dbContext.Restaurants.Attach(restaurant);
            entity.State = EntityState.Modified;
            return restaurant;
        }
    }
}
