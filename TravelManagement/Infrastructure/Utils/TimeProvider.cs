using System;

namespace TravelManagement.Infrastructure.Utils
{
	public interface ITimeProvider
	{
		DateTime UtcNow();
	}
	
	public class TimeProvider : ITimeProvider
	{
		public DateTime UtcNow()
		{
			return DateTime.UtcNow;
		}
	}
}