using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace advicetest.Infrastructure.Interceptors.Services
{
    /// <summary>
    /// Запуск Advice
    /// </summary>
    public class AdviceInvoker : IAdviceInvoker
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Запуск Advice
        /// </summary>
        public AdviceInvoker(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Поиск таргета
        /// </summary>
        public AdviceTarget FindTarget(Type serviceType)
        {
            var interfaceTypes = serviceType.GetInterfaces();
            var advices = interfaceTypes
                .SelectMany(x => _serviceProvider.GetServices(typeof(IAdvice<>).MakeGenericType(x)))
                .Where(x => x != null)
                .Cast<IAdvice>()
                .ToList();

            return new AdviceTarget(advices);
        }
    }
}