using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DxDyIntegrationTest.Data;
using DxDyIntegrationTest.Models;
using DxDyIntegrationTest.Services;

namespace DxDyIntegrationTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private readonly DataService _starWarsService;
        private readonly HttpClient _httpClient;
        private readonly DataContext _context;

        public DataController(DataService starWarsService, HttpClient httpClient, DataContext context)
        {
            _starWarsService = starWarsService;
            _httpClient = httpClient;
            _context = context;
        }

        // Get list of planets
        [HttpGet("planets")]
        public async Task<IActionResult> GetPlanets()
        {
            var planets = await _starWarsService.GetPlanetsAsync();
            return Ok(planets);
        }

        // Get list of ships
        [HttpGet("ships")]
        public async Task<IActionResult> GetShips()
        {
            var ships = await _starWarsService.GetShipsAsync();
            return Ok(ships);
        }

        // Update existing planets and ships
        [HttpPost("submit")]
        public async Task<IActionResult> SubmitData([FromBody] SubmissionData data)
        {
            if (data == null || (data.Planets == null && data.Ships == null))
            {
                return BadRequest("No data provided.");
            }

            // Update planets
            foreach (var planet in data.Planets)
            {
                var existingPlanet = await _context.Planets.FindAsync(planet.Id);
                if (existingPlanet != null)
                {
                    existingPlanet.Name = planet.Name;
                    existingPlanet.Climate = planet.Climate;
                    existingPlanet.Terrain = planet.Terrain;
                }
            }

            // Update ships
            foreach (var ship in data.Ships)
            {
                var existingShip = await _context.Ships.FindAsync(ship.Id);
                if (existingShip != null)
                {
                    existingShip.Name = ship.Name;
                    existingShip.Model = ship.Model;
                    existingShip.Manufacturer = ship.Manufacturer;
                }
            }

            await _context.SaveChangesAsync();

            // Prepare and send data to another system
            var payload = new
            {
                Planets = data.Planets,
                Ships = data.Ships
            };

            var destinationApiUrl = "Test Endpoint";
            var response = await _httpClient.PostAsJsonAsync(destinationApiUrl, payload);

            if (response.IsSuccessStatusCode)
            {
                return Ok("Data successfully submitted.");
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Failed to submit data");
            }
        }
    }

    public class SubmissionData
    {
        public List<Planet> Planets { get; set; }
        public List<Ship> Ships { get; set; }
    }
}
