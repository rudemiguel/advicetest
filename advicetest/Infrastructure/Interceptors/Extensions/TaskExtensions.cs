using System;
using System.Reflection;
using System.Threading.Tasks;

namespace advicetest.Infrastructure.Interceptors.Extensions
{
	public static class TaskExtensions
	{
		private static MethodInfo _methodInfo = typeof(Task).GetMethod(nameof(Task.FromResult));			

		/// <summary>
		/// Преобразуем Task в Task[T] с результатом
		/// </summary>
		public static Task ToTaskT(this Task<object> task, Type resultType = null)
		{
			resultType ??= task.Result.GetType();

			var taskFromResultMethod = _methodInfo.MakeGenericMethod(resultType);

			return taskFromResultMethod.Invoke(null, new[] { task.Result }) as Task;
		}
	}
}
