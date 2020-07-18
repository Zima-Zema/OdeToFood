using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantData restaurantData;

        public RestaurantsController(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }

        // GET: api/Restaurants
        [HttpGet]
        public IEnumerable<Restaurant> GetRestaurants([FromQuery(Name = "name")] string name)
        {
            if (name == null)
            {
                return restaurantData.GetRestaurants();
            }
            return restaurantData.GetRestaurantsByName(name);
        }

        // GET: api/Restaurants/4
        [HttpGet("{id}")]
        public IActionResult GetRestaurant([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Restaurant restaurant = restaurantData.GetRestaurantById(id);
            if (restaurant == null)
            {
                return NotFound();
            }
            return Ok(restaurant);
        }

        // POST: api/Restaurants
        [HttpPost]
        public IActionResult PostRestaurants([FromBody] Restaurant restaurant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Restaurant rest = restaurantData.AddRestaurant(restaurant);
            int response = restaurantData.Commit();
            if (response > 0)
            {
                return Created(new Uri(Request.Path + "/" + rest.Id), rest);
            }
            return NotFound();

        }

        [HttpPut("{id}")]
        public IActionResult PutRestaurants([FromBody] Restaurant rest, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rest.Id)
            {
                return BadRequest();
            }

            Restaurant restaurant = restaurantData.UpdateRestaurant(rest);
            int response = restaurantData.Commit();
            if (response > 0)
            {
                return Ok(restaurant);
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRestaurants([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Restaurant restaurant = restaurantData.DeleteRestaurant(id);
            int response = restaurantData.Commit();
            if (restaurant == null)
            {
                return NotFound();
            }
            return Ok(restaurant);

        }
    }
}