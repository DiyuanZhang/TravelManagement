using System;
using TravelManagement.Domain.Models;

namespace TravelManagement.Test.Fixtures
{
	internal static partial class Fixtures
	{
		public static FlightOrder FlightOrder()
		{
			return new FlightOrder
			{
				FlightOrderRequest = null,
				CreatedAt = DateTime.Parse("2021-12-20 12:00:00"),
			};
		}
	}
}