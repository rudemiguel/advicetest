using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using advicetest.Infrastructure.Interceptors;
using FluentValidation;
using FluentValidation.Results;

namespace advicetest.Validation.Advices
{
	/// <summary>
	/// Адвайз для валидации
	/// </summary>
	public abstract class ValidatingAdvice : IAdvice
	{
		private readonly IServiceProvider _serviceProvider;

		/// <summary>
		/// Адвайз для валидации
		/// </summary>
		public ValidatingAdvice(
			IServiceProvider serviceProvider
		)
        {
			_serviceProvider = serviceProvider;
		}

		/// <inheritdoc/>
		public async Task Before(MethodInfo methodInfo, object[] parameters)
        {
			var validationResults = new List<ValidationResult>();

			// По всем параметрам метода
			foreach (var parameter in parameters)
            {
				var validatorType = typeof(IValidator<>).MakeGenericType(parameter.GetType());

				// Вытаскиваем валидатор из конйнера				
				var validator = _serviceProvider.GetService(validatorType) as IValidator;
				if (validator == null)
					continue;

				// Создаем контекст валидации
				var validationContextType = typeof(ValidationContext<>).MakeGenericType(parameter.GetType());
				var validationContext = Activator.CreateInstance(validationContextType, parameter) as IValidationContext;

				// Валидируем
				var result = await validator.ValidateAsync(validationContext);
				validationResults.Add(result);
			}

			// Tсли есть ошибки выкидываем исключение
			var errors = validationResults
				.SelectMany(x => x.Errors)
				.Select(x =>x.ErrorMessage)
				.ToList();
			if (errors.Count() > 0)
				throw new ValidationException(string.Join(", ", errors));

		}

		/// <inheritdoc/>
		public async Task After(MethodInfo methodInfo, object returnValue, object[] parameters)
        {
        }

		/// <inheritdoc/>
		public async Task Exception(MethodInfo methodInfo, object[] parameters, Exception exception)
        {
        }
	}
}
