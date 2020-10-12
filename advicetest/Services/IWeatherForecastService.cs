using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using interceptors.Contracts;
using interceptors.Infrastructure;

namespace interceptors.Services
{
	/// <summary>
	/// Сервис прогноза погоды
	/// </summary>
	public interface IWeatherForecastService : IApplicationService
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
