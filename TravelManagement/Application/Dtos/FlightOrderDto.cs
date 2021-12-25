using System;
using TravelManagement.Domain.Models;

namespace TravelManagement.Application.Dtos
{
	public class FlightOrderDto
	{
		public virtual long Id { get; set; }
		public virtual long FlightOrderRequestId { get; set; }
		public virtual long UserId { get; set; }
		public virtual string FlightNumber { get; set; }
		public virtual decimal Amount { get; set; }
		public virtual DateTime DepartureDate { get; set; }
		public virtual DateTime CreatedAt { get; set; }

		public static FlightOrderDto Build(FlightOrder flightOrder)
		{
			return new FlightOrderDto
			{
				Id = flightOrder.Id,
				CreatedAt = flightOrder.CreatedAt,
				FlightOrderRequestId = flightOrder.FlightOrderRequest.Id,
				UserId = flightOrder.FlightOrderRequest.UserId,
				FlightNumber = flightOrder.FlightOrderRequest.FlightNumber,
				Amount = flightOrder.FlightOrderRequest.Amount,
				DepartureDate = flightOrder.FlightOrderRequest.DepartureDate
			};
		}
	}
}