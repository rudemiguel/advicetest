using System;
using System.Linq;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using Force.DeepCloner;
using advicetest.Infrastructure.Interceptors.Extensions;
using advicetest.Infrastructure.Interceptors.Services;

namespace advicetest.Infrastructure.Interceptors
{
	/// <summary>
	/// Перехватчик
	/// </summary>
	internal class Interceptor : IInterceptor
	{
		private readonly IAdviceInvoker _adviceInvoker;

		/// <summary>
		/// Перехватчик
		/// </summary>
		public Interceptor(
			IAdviceInvoker adviceInvoker
		)
		{
			_adviceInvoker = adviceInvoker;
		}

		/// <summary>
		/// Перехват
		/// </summary>		
		public void Intercept(IInvocation invocation)
		{
			var returnType = invocation.Method.ReturnType;
			var isAsync = (returnType != null && typeof(Task).IsAssignableFrom(returnType));
			var isTaskT = (isAsync && returnType.IsGenericType);

			// Берем таргет
			var target = _adviceInvoker.FindTarget(invocation.Method.DeclaringType);

			// Асинхронный метод для выполенния метода и таргета адвайзов
			Func<Task<object>> continuation = async () =>
			{
				var myInvocation = invocation.ShallowClone();
				await target.Before(myInvocation.Method, myInvocation.Arguments);

				myInvocation.Proceed();
				try
				{
					if (isAsync)
						await (Task)myInvocation.ReturnValue;

					var resultValue = GetReturnValue(myInvocation.ReturnValue);

					await target.After(myInvocation.Method, resultValue, myInvocation.Arguments);

					return resultValue;
				}
				catch (Exception e)
				{
					await target.Exception(myInvocation.Method, myInvocation.Arguments, e);
					throw;
				}
			};

			// Заворачиваем результат обратно
			var continuationTask = continuation();
			object returnValue = null;

			if (isAsync)
			{
				if (isTaskT)
					returnValue = continuationTask.ToTaskT(returnType.GetGenericArguments().First());
				else
					returnValue = continuationTask as Task;
			}
			else
			{
				returnValue = continuationTask.GetAwaiter().GetResult();
			}				

			invocation.ReturnValue = returnValue;
		}

		/// <summary>
		///  Получаем значение
		/// </summary>
		private object GetReturnValue(dynamic value)
		{
			var valueType = value.GetType();

			if (typeof(Task).IsAssignableFrom(valueType))
			{
				if (valueType.IsGenericType)
					return value.Result;
				return null;
			}

			return value;
		}
	}
}
