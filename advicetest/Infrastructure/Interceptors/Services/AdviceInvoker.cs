using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
        /// Кеш типов для перехватываемых сервисов
        /// </summary>
        private readonly ConcurrentDictionary<Type, List<Type>> _serviceTypesCache =
            new ConcurrentDictionary<Type, List<Type>>();

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
            var interfaceTypes = GetTypes(serviceType);

            var advices = interfaceTypes
                .SelectMany(x => _serviceProvider.GetServices(typeof(IAdvice<>).MakeGenericType(x)))
                .Where(x => x != null)
                .Cast<IAdvice>()
                .ToList();

            return new AdviceTarget(advices);
        }

        /// <summary>
        /// Берем типы для которых искать адвайзы
        /// </summary>
        /// <remarks>
        /// Сортируем типы родительские типы (в том числе интерфейсы в порядке наследования)
        /// вызов адвайзов для предков будет происходить в порядке наследования
        /// </remarks>
        private ICollection<Type> GetTypes(Type type)
        {
            // Ищем в кеше
            List<Type> typeList = null;
            _serviceTypesCache.TryGetValue(type, out typeList);
            if (typeList != null)
                return typeList;

            // Если тип интерфейс - сортируем еерархию интерфейсов
            if (type.IsInterface)
                typeList = TraverseInterfaceHierarchy(type);

            // Если класс
            if (type.IsClass)
            {
                typeList = new List<Type>();

                var curType = type;
                while (curType != null)
                {
                    typeList.Add(curType);
                    var interfaces = TraverseInterfaceHierarchy(curType);
                    typeList.AddRange(interfaces);

                    curType = curType.BaseType;
                }
            }

            // Кладем в кеш и отдаем
            typeList = typeList.Distinct().ToList();
            typeList.Reverse();
            _serviceTypesCache.TryAdd(type, typeList);
            return typeList;
        }

        /// <summary>
        /// Сортируем интерфейсы типа в порядке наследования
        /// </summary>
        private List<Type> TraverseInterfaceHierarchy(Type type)
        {
            var interfaces = type.GetInterfaces();
            return interfaces.OrderBy(x => x.GetInterfaces().Count()).ToList();
        }
    }
}