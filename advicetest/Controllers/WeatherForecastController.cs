using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using advicetest.Contracts;
using advicetest.Services;

namespace advicetest.Controllers
{
    /// <summary>
    /// Коетроллер погоды
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _weatherForecastService;
        private readonly ISimpleWeatherForecastService _simpleWeatherForecastService;
        private readonly ILoadTestingWeatherForecastService _loadTestingWeatherForecastService;

        /// <summary>
        /// Коетроллер погоды
        /// </summary>
        public WeatherForecastController(
            IWeatherForecastService weatherForecastService, 
            ISimpleWeatherForecastService simpleWeatherForecastService, 
            ILoadTestingWeatherForecastService loadTestingWeatherForecastService)
        {
            _weatherForecastService = weatherForecastService;
            _simpleWeatherForecastService = simpleWeatherForecastService;
            _loadTestingWeatherForecastService = loadTestingWeatherForecastService;
        }

        [HttpGet]
        public virtual async Task<IEnumerable<WeatherForecastContract>> Get([FromQuery] int locationId)
        {
            var today = _weatherForecastService.Today();

            return await _weatherForecastService.GetForecast(10);
        }
        
        [HttpGet("Simple")]
        public virtual async Task<IEnumerable<WeatherForecastContract>> GetSimple([FromQuery] int locationId)
        {
            var today = _simpleWeatherForecastService.Today();

            return await _simpleWeatherForecastService.GetForecast(10);
        }
        
        [HttpGet("LoadTesting")]
        public virtual async Task<IEnumerable<WeatherForecastContract>> GetLoadTesting([FromQuery] int locationId)
        {
            var today = _loadTestingWeatherForecastService.Today();

            return await _loadTestingWeatherForecastService.GetForecast(10);
        }  
    }
}