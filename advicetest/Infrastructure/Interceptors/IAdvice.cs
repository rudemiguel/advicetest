using System;
using System.Reflection;
using System.Threading.Tasks;

namespace interceptors.Infrastructure.Interceptors
{
	/// <summary>
	/// Интерфейс для advice
	/// </summary>
	public interface IAdvice
	{
		/// <summary>
		/// Обработчик перед выполнением метода
		/// </summary>
		Task Before(MethodInfo methodInfo, object[] parameters);

		/// <summary>
		/// Обработчик после выполнения метода
		/// </summary>
		Task After(MethodInfo methodInfo, object returnValue, object[] parameters);
		
		/// <summary>
		/// Обработчик при исключении
		/// </summary>
		Task Exception(MethodInfo methodInfo, object[] parameters, Exception exception);
	}

	/// <summary>
	/// Интерфейс для advice
	/// </summary>
	public interface IAdvice<T> : IAdvice
	{
	}
}
