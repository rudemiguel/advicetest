﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using advicetest.Contracts;

namespace advicetest.Services
{
	/// <summary>
	/// Сервис прогноза погоды
	/// </summary>
	public class WeatherForecastService : IWeatherForecastService
	{
		private readonly Random _rng = new Random();

		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		/// <summary>
		/// Сервис прогноза погоды
		/// </summary>
		public WeatherForecastService()
		{
		}

		/// <summary>
		/// Прогноз погоды на нскольео дней
		/// </summary>
		public async Task<IReadOnlyCollection<WeatherForecastResponseContract>> GetForecast(WeatherForecastRequestContract request)
		{
			return Enumerable.Range(1, 5).Select(index => new WeatherForecastResponseContract
			{
				Date = DateTime.Now.AddDays(index),
				TemperatureC = _rng.Next(-20, 55),
				Summary = Summaries[_rng.Next(Summaries.Length)]
			}).ToList().AsReadOnly();
		}

		/// <summary>
		/// Сегодняшний день
		/// </summary>
		public DateTime Today()
		{
			return DateTime.Now;
		}
	}
}
