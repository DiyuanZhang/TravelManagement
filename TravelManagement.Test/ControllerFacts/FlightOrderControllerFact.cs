using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using TravelManagement.Application.Dtos;
using TravelManagement.Application.Exceptions;
using TravelManagement.Application.Services;
using TravelManagement.Infrastructure.Utils;
using TravelManagement.Test.ControllerFactsSetup;
using Xunit;

namespace TravelManagement.Test.ControllerFacts
{
    public class FlightOrderControllerFact : ControllerFactBase
    {
        private readonly Mock<FlightOrderService> _mockFlightOrderService;
        private const long UserId = 100;
        private const long FlightOrderRequestId = 10;
        private const long FlightOrderId = 20;

        public FlightOrderControllerFact()
        {
            _mockFlightOrderService = new Mock<FlightOrderService>();
            Setup(service =>
            {
                service.AddTransient(_ => _mockFlightOrderService.Object);
            });
            HttpClient.DefaultRequestHeaders.Add("UserId", UserId.ToString());
        }
        
        [Theory]
        [InlineData("1001", 100, "2021-10-30")]
        [InlineData("2001", 200, "2021-10-26")]
        public async Task should_return_ok_with_flight_order_request_id_when_create_flight_order_request_with_valid_request(string flightNumber, decimal amount, DateTime departureDate)
        {
            _mockFlightOrderService.Setup(s =>
                s.CreateFlightOrderRequest(It.IsAny<long>(), It.IsAny<CreateFlightOrderRequest>())).Returns(Task.FromResult(FlightOrderRequestId));
            var httpContent = RequestUtils.ToHttpContent(GetDefaultCreateFlightOrderRequest(flightNumber, amount, departureDate));
            var httpResponseMessage = await HttpClient.PostAsync("flight-orders", httpContent);
            httpResponseMessage.EnsureSuccessStatusCode();
            var responseFlightOrderRequestId = await httpResponseMessage.Content.ReadFromJsonAsync<long>();
            Assert.Equal(FlightOrderRequestId, responseFlightOrderRequestId);
        }
        
        [Fact]
        public async Task should_call_flight_order_service_to_create_flight_order_request_when_create_flight_order_request_with_valid_request()
        {
            _mockFlightOrderService.Setup(s =>
                s.CreateFlightOrderRequest(It.IsAny<long>(), It.IsAny<CreateFlightOrderRequest>())).Returns(Task.FromResult(FlightOrderRequestId));
            var defaultCreateFlightOrderRequest = GetDefaultCreateFlightOrderRequest();
            var httpContent = RequestUtils.ToHttpContent(defaultCreateFlightOrderRequest);
            var httpResponseMessage = await HttpClient.PostAsync("flight-orders", httpContent);
            httpResponseMessage.EnsureSuccessStatusCode();
            _mockFlightOrderService.Verify(s => s.CreateFlightOrderRequest(UserId, It.Is<CreateFlightOrderRequest>(r =>
            
                r.Amount == defaultCreateFlightOrderRequest.Amount &&
                r.FlightNumber == defaultCreateFlightOrderRequest.FlightNumber &&
                r.DepartureDate.Date == defaultCreateFlightOrderRequest.DepartureDate.Date
            )));
        }

        [Theory]
        [InlineData(null, 100, "2021-10-30")]
        [InlineData("1001", -1, "2021-10-30")]
        [InlineData("1001", 100, "2020-10-15")]
        public async Task should_return_bad_request_when_create_flight_order_request_with_invalid_request(string flightNumber, decimal amount, DateTime departureDate)
        {
            _mockFlightOrderService.Setup(s =>
                s.CreateFlightOrderRequest(It.IsAny<long>(), It.IsAny<CreateFlightOrderRequest>())).Returns(Task.FromResult(FlightOrderRequestId));
            var defaultCreateFlightOrderRequest = GetDefaultCreateFlightOrderRequest();
            defaultCreateFlightOrderRequest.FlightNumber = flightNumber;
            defaultCreateFlightOrderRequest.Amount = amount;
            defaultCreateFlightOrderRequest.DepartureDate = departureDate;
            var httpContent = RequestUtils.ToHttpContent(defaultCreateFlightOrderRequest);
            var httpResponseMessage = await HttpClient.PostAsync("flight-orders", httpContent);
            Assert.Equal(HttpStatusCode.BadRequest, httpResponseMessage.StatusCode);
        }

        [Fact]
        public async Task should_return_ok_with_flight_order_id_when_confirm_flight_order_with_valid_request()
        {
            _mockFlightOrderService.Setup(s =>
                s.ConfirmFlightOrderRequest(It.IsAny<long>())).Returns(FlightOrderId);
            var httpResponseMessage = await HttpClient.PostAsync($"flight-orders/{10}/confirmation", null!);
            httpResponseMessage.EnsureSuccessStatusCode();
            var responseFlightOrderId = await httpResponseMessage.Content.ReadFromJsonAsync<long>();
            Assert.Equal(FlightOrderId, responseFlightOrderId);
        }

        [Fact]
        public async Task should_return_503_when_create_flight_order_request_method_throw_ServiceUnavailableException()
        {
            _mockFlightOrderService.Setup(s =>
                    s.CreateFlightOrderRequest(It.IsAny<long>(), It.IsAny<CreateFlightOrderRequest>()))
                .Throws(new ServiceUnavailableException("error"));
            var httpContent = RequestUtils.ToHttpContent(GetDefaultCreateFlightOrderRequest());
            var httpResponseMessage = await HttpClient.PostAsync("flight-orders", httpContent);
            Assert.Equal(HttpStatusCode.ServiceUnavailable, httpResponseMessage.StatusCode);
        }

        private CreateFlightOrderRequest GetDefaultCreateFlightOrderRequest(string flightNumber = null, decimal? amount = null, DateTime? departureDate = null)
        {
            return new()
            {
                FlightNumber = flightNumber ?? "1001",
                Amount = amount ?? 100,
                DepartureDate = departureDate?? DateTime.Parse("2021-10-30")
            };
        }
    }
}
