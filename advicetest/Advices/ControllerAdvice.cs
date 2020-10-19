using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using advicetest.Infrastructure.Interceptors;

namespace advicetest.Advices
{
    /// <summary>
    /// Адвайз для контроллеров
    /// </summary>
    public class ControllerAdvice : IAdvice<ControllerBase>
    {
        private readonly ILogger<ControllerAdvice> _logger;

        /// <summary>
        /// Адвайз для сервисов приложения
        /// </summary>
        public ControllerAdvice(
            ILogger<ControllerAdvice> logger
        )
        {
            _logger = logger;
        }

        /// <summary>
        /// Обработчик перед выполнением метода
        /// </summary>
        public async Task Before(MethodInfo methodInfo, object[] parameters)
        {
            _logger.LogInformation("Перед вызовом метода контроллера {@methodName}", methodInfo.Name);
        }

        /// <summary>
        /// Обработчик после выполнения метода
        /// </summary>
        public async Task After(MethodInfo methodInfo, object returnValue, object[] parameters)
        {
            _logger.LogInformation("После вызовом метода контроллера {@methodName}", methodInfo.Name);
        }

        /// <summary>
        /// Обработчик при исключении
        /// </summary>
        public async Task Exception(MethodInfo methodInfo, object[] parameters, Exception exception)
        {
        }
    }
}