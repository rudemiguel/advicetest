using System;
namespace advicetest.Infrastructure.Exceptions
{
	/// <summary>
	/// Исключение
	/// </summary>
	public class BaseException : Exception
	{
		/// <summary>
		/// Исключение
		/// </summary>
		public BaseException(string message)
			: base(message)
		{
		}
	}
}
