using System;
namespace advicetest.Contracts
{
	/// <summary>
	/// Запрос на прогноз погоды
	/// </summary>
	public class WeatherForecastRequestContract
	{
		public int LocationId { get; set; }

		public int Days { get; set; }
	}
}
