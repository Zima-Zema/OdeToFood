using System.Collections.Generic;
using System.Linq;
using OdeToFood.Core;

namespace OdeToFood.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant { Id = 1, Name = "restaurent 1", Location = "Cairo", Cuisine = CuisineType.Mexican },
                new Restaurant { Id = 2, Name = "restaurent 2", Location = "Giza", Cuisine = CuisineType.Indian },
                new Restaurant { Id = 3, Name = "restaurent 3", Location = "Alex", Cuisine = CuisineType.Italian },
                new Restaurant { Id = 4, Name = "restaurent 4", Location = "Maadi", Cuisine = CuisineType.None },
                new Restaurant { Id = 5, Name = "restaurent 5", Location = "Nasr City", Cuisine = CuisineType.Asian },
                new Restaurant { Id = 6, Name = "restaurent 6", Location = "October", Cuisine = CuisineType.Egyptian },
            };
        }
        public IEnumerable<Restaurant> GetRestaurants()
        {
            return from r in restaurants
                   orderby r.Name
                   select r;
        }

        public Restaurant GetRestaurantById(int id)
        {
            return restaurants.SingleOrDefault(res => res.Id == id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }

        public Restaurant UpdateRestaurant(Restaurant restaurant)
        {
            Restaurant restaurantObj = restaurants.SingleOrDefault(res => res.Id == restaurant.Id);
            if (restaurantObj != null)
            {
                restaurantObj.Name = restaurant.Name;
                restaurantObj.Location = restaurant.Location;
                restaurantObj.Cuisine = restaurant.Cuisine;
            }
            return restaurantObj;
        }

        public int Commit()
        {
            return 0;
        }

        public Restaurant AddRestaurant(Restaurant restaurant)
        {
            restaurant.Id = restaurants.Max(res => res.Id) + 1;
            restaurants.Add(restaurant);
            return restaurant;
        }

        public Restaurant DeleteRestaurant(int id)
        {
           Restaurant restaurant = restaurants.SingleOrDefault(res => res.Id == id);
           if (restaurant != null)
           {
               restaurants.Remove(restaurant);
           }
           return restaurant;
        }

        public int CountRestaurant()
        {
            return restaurants.Count();
        }
    }
}
