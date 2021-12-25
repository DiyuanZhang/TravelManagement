using System;
using TravelManagement.Domain.Models;

namespace TravelManagement.Test.Fixtures
{
	internal static partial class Fixtures
	{
		public static FlightOrderRequest FlightOrderRequest()
		{
			return new FlightOrderRequest
			{
				UserId = 1L,
				FlightNumber = "001",
				Amount = 100,
				DepartureDate = DateTime.Parse("2021-12-25 12:00:00"),
				CreatedAt = DateTime.Parse("2021-12-20 12:00:00"),
				ExpiredAt = DateTime.Parse("2021-12-20 12:30:00"),
			};
		}
	}
}