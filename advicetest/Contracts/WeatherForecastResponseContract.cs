﻿using System;

namespace advicetest.Contracts
{
	/// <summary>
	/// Контракт погоды
	/// </summary>
	public class WeatherForecastResponseContract
	{
		public DateTime Date { get; set; }

		public int TemperatureC { get; set; }

		public int TemperatureF { get; set; }

		public string Summary { get; set; }
	}
}
