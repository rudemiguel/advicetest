using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using advicetest.Contracts;
using advicetest.Services;

namespace advicetest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _weatherForecastService;
        private readonly ISimpleWeatherForecastService _simpleWeatherForecastService;
        private readonly ILoadTestingWeatherForecastService _loadTestingWeatherForecastService;

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
        public async Task<IEnumerable<WeatherForecastContract>> Get()
        {
            return await _weatherForecastService.GetForecast(10);
        }
        
        [HttpGet("Simple")]
        public async Task<IEnumerable<WeatherForecastContract>> GetSimple()
        {
            return await _simpleWeatherForecastService.GetForecast(10);
        }
        
        [HttpGet("LoadTesting")]
        public async Task<IEnumerable<WeatherForecastContract>> GetLoadTesting()
        {
            return await _loadTestingWeatherForecastService.GetForecast(10);
        }        
    }
}