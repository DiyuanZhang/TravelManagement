using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using TravelManagement.Application.Dtos;
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
            _mockFlightOrderService.Setup(s =>
                s.CreateFlightOrderRequest(It.IsAny<long>(), It.IsAny<CreateFlightOrderRequest>())).Returns(FlightOrderRequestId);
            _mockFlightOrderService.Setup(s =>
                s.ConfirmFlightOrderRequest(It.IsAny<long>())).Returns(FlightOrderId);
            Setup(service =>
            {
                service.AddTransient(_ => _mockFlightOrderService.Object);
            });
            HttpClient.DefaultRequestHeaders.Add("UserId", UserId.ToString());
        }
        
        [Fact]
        public async Task should_return_ok_with_flight_order_request_id_when_create_flight_order_request_with_valid_request()
        {
            var httpContent = RequestUtils.ToHttpContent(GetDefaultCreateFlightOrderRequest());
            var httpResponseMessage = await HttpClient.PostAsync("flight-orders", httpContent);
            httpResponseMessage.EnsureSuccessStatusCode();
            var responseFlightOrderRequestId = await httpResponseMessage.Content.ReadFromJsonAsync<long>();
            Assert.Equal(FlightOrderRequestId, responseFlightOrderRequestId);
        }

        [Theory]
        [InlineData(null, 100, "2021-12-25")]
        [InlineData("001", -1, "2021-12-25")]
        [InlineData("001", 100, "2020-12-25")]
        public async Task should_return_bad_request_when_create_flight_order_request_with_invalid_request(string flightNumber, decimal amount, DateTime departureDate)
        {
            var defaultCreateFlightOrderRequest = GetDefaultCreateFlightOrderRequest();
            defaultCreateFlightOrderRequest.FlightNumber = flightNumber;
            defaultCreateFlightOrderRequest.Amount = amount;
            defaultCreateFlightOrderRequest.DepartureDate = departureDate;
            var httpContent = RequestUtils.ToHttpContent(defaultCreateFlightOrderRequest);
            var httpResponseMessage = await HttpClient.PostAsync("flight-orders", httpContent);
            Assert.Equal(HttpStatusCode.BadRequest, httpResponseMessage.StatusCode);
        }
        
        [Fact]
        public async Task should_call_flight_order_service_to_create_flight_order_request_when_create_flight_order_request_with_valid_request()
        {
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

        [Fact]
        public async Task should_return_ok_with_flight_order_id_when_confirm_flight_order_with_valid_request()
        {
            var httpResponseMessage = await HttpClient.PostAsync($"flight-orders/{10}/confirmation", null!);
            httpResponseMessage.EnsureSuccessStatusCode();
            var responseFlightOrderId = await httpResponseMessage.Content.ReadFromJsonAsync<long>();
            Assert.Equal(FlightOrderId, responseFlightOrderId);
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
