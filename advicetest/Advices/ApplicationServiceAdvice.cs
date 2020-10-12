using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using interceptors.Infrastructure;
using interceptors.Infrastructure.Interceptors;

namespace interceptors.Advices
{
	/// <summary>
	/// Адвайз для сервисов приложения
	/// </summary>
	public class ApplicationServiceAdvice : IAdvice<IApplicationService>
	{
		private readonly ILogger<ApplicationServiceAdvice> _logger;

		/// <summary>
		/// Адвайз для сервисов приложения
		/// </summary>
		public ApplicationServiceAdvice(
			ILogger<ApplicationServiceAdvice> logger
		)
		{
			_logger = logger;
		}

		/// <summary>
		/// Обработчик перед выполнением метода
		/// </summary>
		public async Task Before(MethodInfo methodInfo, object[] parameters)
		{
			_logger.LogInformation("Перед вызовом метода {@methodName}", methodInfo.Name);
		}

		/// <summary>
		/// Обработчик после выполнения метода
		/// </summary>
		public async Task After(MethodInfo methodInfo, object returnValue, object[] parameters)
		{
			_logger.LogInformation("После вызовом метода {@methodName}", methodInfo.Name);
		}

		/// <summary>
		/// Обработчик при исключении
		/// </summary>
		public async Task Exception(MethodInfo methodInfo, object[] parameters, Exception exception)
		{
		}
	}
}
