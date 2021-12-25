using System;
using TravelManagement.Domain.Models;
using TravelManagement.Domain.Repositories;

namespace TravelManagement.Domain.Services
{
	public class FlightOrderDomainService
	{
		private readonly IFlightOrderRequestRepository _flightOrderRequestRepository;
		private readonly IFlightOrderRepository _flightOrderRepository;

		public FlightOrderDomainService()
		{
		}
		
		public FlightOrderDomainService(IFlightOrderRequestRepository flightOrderRequestRepository, IFlightOrderRepository flightOrderRepository)
		{
			_flightOrderRequestRepository = flightOrderRequestRepository;
			_flightOrderRepository = flightOrderRepository;
		}

		public virtual FlightOrderRequest CreateFlightOrderRequest(long userId, string flightNumber, decimal amount,
			DateTime departureDate)
		{
			var flightOrderRequest = new FlightOrderRequest
			{
				UserId = userId,
				FlightNumber = flightNumber,
				Amount = amount,
				DepartureDate = departureDate
			};
			flightOrderRequest.Create();
			return _flightOrderRequestRepository.Create(flightOrderRequest);
		}
		
		public virtual FlightOrder ConfirmFlightOrderRequest(long flightOrderRequestId)
		{
			var flightOrderRequest = _flightOrderRequestRepository.FindById(flightOrderRequestId);
			var flightOrder = new FlightOrder
			{
				FlightOrderRequest = flightOrderRequest
			};
			flightOrder.Create();
			return _flightOrderRepository.Create(flightOrder);
		}
	}
}