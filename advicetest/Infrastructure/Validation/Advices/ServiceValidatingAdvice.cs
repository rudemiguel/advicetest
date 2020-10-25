using System;
using advicetest.Infrastructure.Interceptors;
using advicetest.Validation.Advices;

namespace advicetest.Infrastructure.Validation.Advices
{
	/// <summary>
	/// Адвайз для валидации параметров сервисов
	/// </summary>
	public class ServiceValidatingAdvice : ValidatingAdvice, IAdvice<IApplicationService>
	{
		/// <summary>
		/// Адвайз для валидации параметров сервисов
		/// </summary>
		public ServiceValidatingAdvice(
			IServiceProvider serviceProvider
		) : base(serviceProvider)
        {
        }
	}
}
