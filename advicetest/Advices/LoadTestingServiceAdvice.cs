using System;
using System.Reflection;
using System.Threading.Tasks;
using advicetest.Infrastructure;
using advicetest.Infrastructure.Interceptors;
using Microsoft.Extensions.Logging;

namespace advicetest.Advices
{
    public class LoadTestingServiceAdvice : IAdvice<ILoadTestingService>
    {
        private readonly ILogger<LoadTestingServiceAdvice> _logger;

        public LoadTestingServiceAdvice(ILogger<LoadTestingServiceAdvice> logger)
        {
            _logger = logger;
        }

        public async Task Before(MethodInfo methodInfo, object[] parameters)
        {
            _logger.LogInformation("Перед вызовом метода {@methodName}", methodInfo.Name);
        }

        public async Task After(MethodInfo methodInfo, object returnValue, object[] parameters)
        {
            _logger.LogInformation("После вызова метода {@methodName}", methodInfo.Name);
        }

        public async Task Exception(MethodInfo methodInfo, object[] parameters, Exception exception)
        {
        }
    }
}