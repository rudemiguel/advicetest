using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace interceptors.Infrastructure.Interceptors.Services
{
	/// <summary>
	/// Таргет адвайзов
	/// </summary>
	public class AdviceTarget
	{
		public ICollection<IAdvice> _advices;

		public AdviceTarget(ICollection<IAdvice> advices)
		{
			_advices = advices;
		}

		/// <summary>
		/// Перед выполнением
		/// </summary>
		public async Task Before(MethodInfo method, object[] arguments)
		{
			foreach (var advice in _advices)
				await advice.Before(method, arguments);
		}

		/// <summary>
		/// После
		/// </summary>
		public async Task After(MethodInfo method, object returnValue, object[] arguments)
		{
			foreach (var advice in _advices)
				await advice.After(method, returnValue, arguments);
		}

		/// <summary>
		/// Ошибка
		/// </summary>
		public async Task Exception(MethodInfo method, object[] arguments, Exception exception)
		{
			foreach (var advice in _advices)
				await advice.Exception(method, arguments, exception);
		}

	}
}
