using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using advicetest.Contracts;
using advicetest.Infrastructure;

namespace advicetest.Services
{
    /// <summary>
    /// Сервис прогноза погоды для нагрузочного тестирования
    /// </summary>
    public interface ILoadTestingWeatherForecastService : ILoadTestingService
    {
        /// <summary>
        /// Прогноз погоды на несколько дней
        /// </summary>
        Task<IReadOnlyCollection<WeatherForecastContract>> GetForecast(int days);

        /// <summary>
        /// Сегодняшний день
        /// </summary>
        DateTime Today();        
    }
}