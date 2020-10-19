using System;
using System.Linq;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace advicetest.Infrastructure.Interceptors.Services
{
    /// <summary>
    /// Фабрика контроллеров
    /// </summary>
    public struct ControllerFactory : IControllerFactory
    {
        private readonly IControllerFactory _controllerFactory;
        private readonly IServiceProvider _serviceProvider;
        private static ProxyGenerator _proxyGenerator = new ProxyGenerator();

        /// <summary>
        /// Фабрика контроллеров
        /// </summary>
        public ControllerFactory(
            IControllerFactory controllerFactory,
            IServiceProvider serviceProvider
        )
        {
            _controllerFactory = controllerFactory;
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc/>
        public object CreateController(ControllerContext context)
        {
            var controllerType = context.ActionDescriptor.ControllerTypeInfo.AsType();
            var controller = _controllerFactory.CreateController(context);

            // Интерцептор
            var interceptor = ActivatorUtilities.CreateInstance<Interceptor>(_serviceProvider);

            // Прокси
            var constructorInfo = controllerType.GetConstructors().FirstOrDefault();
            var constructorParametrCount = constructorInfo.GetParameters().Count();
            var controllerProxy =_proxyGenerator.CreateClassProxyWithTarget(
                    controllerType,
                    controller,
                    new object[constructorParametrCount],
                    interceptor
                );

            return controllerProxy;
        }

        /// <inheritdoc/>
        public void ReleaseController(ControllerContext context, object controller)
        {
            _controllerFactory.ReleaseController(context, controller);
        }
    }
}
