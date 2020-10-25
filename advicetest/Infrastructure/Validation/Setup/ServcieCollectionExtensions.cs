using System;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace advicetest.Infrastructure.Validation.Setup
{
	public static class ServcieCollectionExtensions
	{
		/// <summary>
		/// Настройка перехватчиков для сервисов наследованных от T
		/// </summary>
		public static IServiceCollection AddValidation(this IServiceCollection services)
		{
			// Добавляем валидаторы
			services.Scan(scan => scan
				.FromCallingAssembly()
				.AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
				.AsImplementedInterfaces()
				.WithTransientLifetime()
			);

			return services;
		}
	}
}
