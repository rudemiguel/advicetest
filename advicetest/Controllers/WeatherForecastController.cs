﻿using System.Collections.Generic;
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

		public WeatherForecastController(
			IWeatherForecastService weatherForecastService			
		)
		{
			_weatherForecastService = weatherForecastService;
		}

		[HttpGet]
		public async Task<IEnumerable<WeatherForecastContract>> Get()
		{
			var today = _weatherForecastService.Today();

			return await _weatherForecastService.GetForecast(10);
		}
	}
}