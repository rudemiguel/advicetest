using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Castle.DynamicProxy;
using advicetest.Infrastructure.Interceptors.Services;

namespace advicetest.Infrastructure.Interceptors.Setup
{
    /// <summary>
    /// Настройка перехватчиков
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        private static readonly ProxyGenerator Generator = new ProxyGenerator();

        /// <summary>
        /// Число копий <see cref="IAdvice"/> в IoT контейнере
        /// </summary>
        private static int NumberOfAdviceCopies => 10;

        /// <summary>
        /// Настройка перехватчиков для сервисов наследованных от T
        /// </summary>
        public static IServiceCollection AddAdvicesOf<T>(this IServiceCollection services)
            where T : class
        {
            // Вытаскиваем сервисы кт наслежованный от T
            var proxyServiceDescriptors = services
                .Where(x => typeof(T).IsAssignableFrom(x.ServiceType))
                .ToList();

            // Заменяем дескрипторы сервисов на нашу фабрику
            foreach (var serviceDescriptor in proxyServiceDescriptors)
            {
                services.Remove(serviceDescriptor);

                var proxyServiceDescriptor = new ServiceDescriptor(serviceDescriptor.ServiceType, (sp) => ServiceFactory(sp, serviceDescriptor), serviceDescriptor.Lifetime);

                services.Add(proxyServiceDescriptor);
            }

            // Добавляем сервис
            services.AddSingleton<IAdviceInvoker, AdviceInvoker>();

            // Добавляем адвайзы
            Enumerable.Range(0, NumberOfAdviceCopies)
                .ToList()
                .ForEach(x =>
                    services.Scan(scan => scan
                        .FromApplicationDependencies()
                        .AddClasses(classes => classes.AssignableTo(typeof(IAdvice<>)))
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()
                    )
                );

            return services;
        }

        /// <summary>
        /// Фабрика создания экземпляра сервиса
        /// </summary>
        private static object ServiceFactory(IServiceProvider serviceProvider, ServiceDescriptor serviceDescriptor)
        {
            var service = ActivatorUtilities.CreateInstance(serviceProvider, serviceDescriptor.ImplementationType);
            var interceptor = ActivatorUtilities.CreateInstance<Interceptor>(serviceProvider);

            return Generator.CreateInterfaceProxyWithTarget(serviceDescriptor.ServiceType, service, interceptor);
        }
    }
}