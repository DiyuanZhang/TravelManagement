using System;

namespace TravelManagement.Application.Exceptions
{
	public class ServiceUnavailableException : Exception
	{
		public ServiceUnavailableException(string message) : base(message)
		{
		}
	}
}