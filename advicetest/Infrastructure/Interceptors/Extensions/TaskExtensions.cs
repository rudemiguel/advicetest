using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace advicetest.Infrastructure.Interceptors.Extensions
{
	public static class TaskExtensions
	{
		private static MethodInfo _resultMethodInfo = typeof(Task).GetMethod(nameof(Task.FromResult));			
		private static MethodInfo _exceptionMethodInfo = typeof(Task).GetMethods().
			FirstOrDefault(x => x.Name == nameof(Task.FromException) && x.IsGenericMethod);

		/// <summary>
		/// Преобразуем Task в Task[T] с результатом
		/// </summary>
		public static Task ToTaskT(this Task<object> task, Type resultType = null)
		{
			resultType ??= task.Result.GetType();

			// Если таск с ошибой создаем из ошибки
			if (task.IsFaulted)
			{
				var taskFromExceptionMethod = _exceptionMethodInfo.MakeGenericMethod(resultType);
				return taskFromExceptionMethod.Invoke(null, new[] { task.Exception }) as Task;
			}

			// Если результат создаем из результата
			var taskFromResultMethod = _resultMethodInfo.MakeGenericMethod(resultType);
			return taskFromResultMethod.Invoke(null, new[] { task.Result }) as Task;
		}
	}
}
