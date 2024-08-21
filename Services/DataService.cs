using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DxDyIntegrationTest.Models;
using System.Collections.Generic;

namespace DxDyIntegrationTest.Services
{
    public class DataService
    {
        private readonly HttpClient _httpClient;

        public DataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Planet>> GetPlanetsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<SWAPIResponse<Planet>>("https://swapi.dev/api/planets/");
            var planets = response.Results;

            // Generate IDs for planets
            for (int i = 0; i < planets.Count; i++)
            {
                if (planets[i].Id == 0) // Check if ID is not set
                {
                    planets[i].Id = i + 1; // Assign a unique ID
                }
            }

            return planets;
        }

        public async Task<List<Ship>> GetShipsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<SWAPIResponse<Ship>>("https://swapi.dev/api/starships/");
            var ships = response.Results;

            // Generate IDs for ships
            for (int i = 0; i < ships.Count; i++)
            {
                if (ships[i].Id == 0) // Check if ID is not set
                {
                    ships[i].Id = i + 1; // Assign a unique ID
                }
            }

            return ships;
        }
    }

    public class SWAPIResponse<T>
    {
        public List<T> Results { get; set; }
    }
}
