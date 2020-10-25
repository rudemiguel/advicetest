using System;
using advicetest.Infrastructure.Exceptions;

namespace advicetest.Infrastructure.Validation.Exceptions
{
	/// <summary>
	/// Исключение валидации
	/// </summary>
	public class ValidationException : BaseException
	{
		/// <summary>
		/// Исключение валидации
		/// </summary>
		public ValidationException(string message)
			: base(message)
		{
		}
	}
}
