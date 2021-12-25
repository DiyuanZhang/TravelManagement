using System;
using Moq;
using TravelManagement.Application.Dtos;
using TravelManagement.Application.Providers;
using TravelManagement.Application.Services;
using TravelManagement.Domain.Services;
using Xunit;

namespace TravelManagement.Test.ApplicationServiceFacts
{
	public class FlightOrderServiceFact
	{
		[Fact]
		public void should_call_domain_service_and_approval_system_when_create_flight_order_request()
		{
			long userId = 1L;
			long flightOrderRequestId = 10L;
			var defaultCreateFlightOrderRequest = GetDefaultCreateFlightOrderRequest();
			
			var _mockFlightOrderDomainService = new Mock<FlightOrderDomainService>();
			_mockFlightOrderDomainService.Setup(s => s.CreateFlightOrderRequest(userId,
				defaultCreateFlightOrderRequest.FlightNumber, defaultCreateFlightOrderRequest.Amount,
				defaultCreateFlightOrderRequest.DepartureDate)).Returns(flightOrderRequestId);
			
			var _mockApprovalSystemProvider = new Mock<IApprovalSystemProvider>();
			
			var flightOrderService = new FlightOrderService(_mockFlightOrderDomainService.Object, _mockApprovalSystemProvider.Object);
			flightOrderService.CreateFlightOrderRequest(userId, defaultCreateFlightOrderRequest);

			_mockFlightOrderDomainService.Verify(s => s.CreateFlightOrderRequest(userId, defaultCreateFlightOrderRequest.FlightNumber, defaultCreateFlightOrderRequest.Amount, defaultCreateFlightOrderRequest.DepartureDate));
			_mockApprovalSystemProvider.Verify(p => p.Approve(userId, It.Is<ApproveRequest>(r => 
				r.OrderRequestId == flightOrderRequestId &&
				r.Amount == defaultCreateFlightOrderRequest.Amount
			)));
		}
		
		private CreateFlightOrderRequest GetDefaultCreateFlightOrderRequest()
		{
			return new()
			{
				FlightNumber = "001",
				Amount = 100,
				DepartureDate = DateTime.UtcNow.AddDays(1)
			};
		}
	}
}