using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using advicetest.Contracts;

namespace advicetest.Services
{
    public class LoadTestingWeatherForecastService : ILoadTestingWeatherForecastService
    {
        private readonly Random _rng = new Random();

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        /// <summary>
        /// Сервис прогноза погоды
        /// </summary>
        public LoadTestingWeatherForecastService()
        {
        }

        /// <summary>
        /// Прогноз погоды на несколько дней
        /// </summary>
        public async Task<IReadOnlyCollection<WeatherForecastContract>> GetForecast(int days)
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecastContract
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