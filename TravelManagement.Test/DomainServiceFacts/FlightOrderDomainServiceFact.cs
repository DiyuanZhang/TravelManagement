using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using TravelManagement.Domain.Models;
using TravelManagement.Domain.Services;
using TravelManagement.Test.DomainServiceFactsSetup;
using Xunit;

namespace TravelManagement.Test.DomainServiceFacts
{
	public class FlightOrderDomainServiceFact : DomainServiceFactBase
	{
		[Fact]
		public void should_create_flight_order_request_when_call_create_flight_order_request()
		{
			var userId = 1L;
			var flightNumber = "001";
			var amount = 100;
			var departureDate = DateTime.UtcNow.AddDays(1);
			var flightOrderDomainService = Provider.GetRequiredService<FlightOrderDomainService>();
			var flightOrderRequestId = flightOrderDomainService.CreateFlightOrderRequest(userId, flightNumber, amount, departureDate);
			var flightOrderRequest = DbQuery<FlightOrderRequest>().SingleOrDefault(r => r.Id == flightOrderRequestId);
			Assert.NotNull(flightOrderRequest);
			Assert.Equal(userId, flightOrderRequest.UserId);
			Assert.Equal(flightNumber, flightOrderRequest.FlightNumber);
			Assert.Equal(amount, flightOrderRequest.Amount);
			Assert.Equal(departureDate.Date, flightOrderRequest.DepartureDate.Date);
		}
	}
}