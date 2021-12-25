using System;

namespace TravelManagement.Application.Dtos
{
	public class CreateFlightOrderRequest
	{
		public string FlightNumber { get; set; }
		public decimal Amount { get; set; }
		public DateTime DepartureDate { get; set; }
	}
}