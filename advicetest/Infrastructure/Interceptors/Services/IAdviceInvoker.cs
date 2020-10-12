using System;

namespace advicetest.Infrastructure.Interceptors.Services
{
	/// <summary>
	/// Запускатель Advice
	/// </summary>
	public interface IAdviceInvoker
	{
		/// <summary>
		/// Поиск таргета
		/// </summary>
		AdviceTarget FindTarget(Type serviceType);
	}
}
