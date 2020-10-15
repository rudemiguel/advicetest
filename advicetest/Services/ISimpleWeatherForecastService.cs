using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using advicetest.Contracts;

namespace advicetest.Services
{
    /// <summary>
    /// Сервис прогноза погода без аспектных дополнений
    /// </summary>
    public interface ISimpleWeatherForecastService
    {
        /// <summary>
        /// Прогноз погоды на нскольео дней
        /// </summary>
        Task<IReadOnlyCollection<WeatherForecastContract>> GetForecast(int days);

        /// <summary>
        /// Сегодняшний день
        /// </summary>
        DateTime Today();
    }
}