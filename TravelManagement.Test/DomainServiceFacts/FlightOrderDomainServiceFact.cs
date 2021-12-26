using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using TravelManagement.Domain.Models;
using TravelManagement.Domain.Services;
using TravelManagement.Test.DomainServiceFactsSetup;
using Xunit;
using static TravelManagement.Test.Fixtures.Fixtures;

namespace TravelManagement.Test.DomainServiceFacts
{
	public class FlightOrderDomainServiceFact : DomainServiceFactBase
	{
		[Fact]
		public void should_create_flight_order_request_when_call_create_flight_order_request()
		{
			var userId = 1L;
			var flightNumber = "1001";
			var amount = 100;
			var departureDate = DateTime.Parse("2021-10-30");
			var flightOrderDomainService = Provider.GetRequiredService<FlightOrderDomainService>();
			var flightOrderRequest = flightOrderDomainService.CreateFlightOrderRequest(userId, flightNumber, amount, departureDate);
			var dbFlightOrderRequest = DbQuery<FlightOrderRequest>().SingleOrDefault(r => r.Id == flightOrderRequest.Id);
			Assert.NotNull(dbFlightOrderRequest);
			Assert.Equal(userId, dbFlightOrderRequest.UserId);
			Assert.Equal(flightNumber, dbFlightOrderRequest.FlightNumber);
			Assert.Equal(amount, dbFlightOrderRequest.Amount);
			Assert.Equal(departureDate.Date, dbFlightOrderRequest.DepartureDate.Date);
			Assert.Equal(DateTime.UtcNow.Date, dbFlightOrderRequest.CreatedAt.Date);
			Assert.Equal(DateTime.UtcNow.Date, dbFlightOrderRequest.ExpiredAt.Date);
			Assert.Equal(30, (int)(dbFlightOrderRequest.ExpiredAt - dbFlightOrderRequest.CreatedAt).TotalMinutes);
		}
		
		[Fact]
		public void should_create_flight_order_when_call_confirm_flight_order_request()
		{
			var flightOrderRequest = Build(FlightOrderRequest()).Default();
			DbInsert(flightOrderRequest);
			
			var flightOrderDomainService = Provider.GetRequiredService<FlightOrderDomainService>();
			var flightOrder = flightOrderDomainService.ConfirmFlightOrderRequest(flightOrderRequest.Id);
			
			var dbFlightOrder = DbQuery<FlightOrder>().SingleOrDefault(r => r.Id == flightOrder.Id);
			Assert.NotNull(dbFlightOrder);
			Assert.Equal(flightOrderRequest.UserId, dbFlightOrder.FlightOrderRequest.UserId);
			Assert.Equal(flightOrderRequest.FlightNumber, dbFlightOrder.FlightOrderRequest.FlightNumber);
			Assert.Equal(flightOrderRequest.Amount, dbFlightOrder.FlightOrderRequest.Amount);
			Assert.Equal(flightOrderRequest.DepartureDate.Date, dbFlightOrder.FlightOrderRequest.DepartureDate.Date);
			Assert.Equal(flightOrderRequest.CreatedAt.Date, dbFlightOrder.FlightOrderRequest.CreatedAt.Date);
			Assert.Equal(flightOrderRequest.ExpiredAt.Date, dbFlightOrder.FlightOrderRequest.ExpiredAt.Date);
			Assert.Equal(DateTime.UtcNow.Date, dbFlightOrder.CreatedAt.Date);
		}
	}
}