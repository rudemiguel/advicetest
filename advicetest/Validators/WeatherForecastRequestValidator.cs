using System;
using FluentValidation;
using advicetest.Contracts;

namespace advicetest.Validators
{
	/// <summary>
	/// Валидатор на запрос погоды
	/// </summary>
	public class WeatherForecastRequestValidator
		: AbstractValidator<WeatherForecastRequestContract>
	{
		public WeatherForecastRequestValidator()
		{
			RuleFor(x => x.Days).GreaterThan(0);
		}
	}
}
