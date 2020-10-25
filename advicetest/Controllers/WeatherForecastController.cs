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

		/// <summary>
		/// Коетроллер погоды
		/// </summary>
		public WeatherForecastController(
			IWeatherForecastService weatherForecastService			
		)
		{
			_weatherForecastService = weatherForecastService;
		}

		[HttpGet]
		public virtual async Task<IEnumerable<WeatherForecastResponseContract>> Get([FromQuery] WeatherForecastRequestContract request)
		{
			return await _weatherForecastService.GetForecast(request);
		}
	}
}
