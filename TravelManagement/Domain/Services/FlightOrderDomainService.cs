using System;
using TravelManagement.Domain.Models;
using TravelManagement.Domain.Repositories;

namespace TravelManagement.Domain.Services
{
	public class FlightOrderDomainService
	{
		private readonly IFlightOrderRequestRepository _flightOrderRequestRepository;

		public FlightOrderDomainService()
		{
			
		}
		
		public FlightOrderDomainService(IFlightOrderRequestRepository flightOrderRequestRepository)
		{
			_flightOrderRequestRepository = flightOrderRequestRepository;
		}

		public virtual long CreateFlightOrderRequest(long userId, string flightNumber, decimal amount, DateTime departureDate)
		{
			var flightOrderRequest = new FlightOrderRequest
			{
				UserId = userId,
				FlightNumber = flightNumber,
				Amount = amount,
				DepartureDate = departureDate
			};
			flightOrderRequest.Create();
			return _flightOrderRequestRepository.Create(flightOrderRequest).Id;
		}
	}
}