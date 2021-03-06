using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using TravelManagement.Application.Dtos;
using TravelManagement.Application.Exceptions;
using TravelManagement.Application.Providers;
using TravelManagement.Application.Services;
using TravelManagement.Domain.Services;
using TravelManagement.Test.ApplicationFactsSetup;
using Xunit;
using static TravelManagement.Test.Fixtures.Fixtures;
using FlightOrderRequest = TravelManagement.Domain.Models.FlightOrderRequest;

namespace TravelManagement.Test.ApplicationServiceFacts
{
	public class FlightOrderServiceFact
	{
		[Fact]
		public void should_call_domain_service_and_approval_system_when_create_flight_order_request()
		{
			const long userId = 1L;
			const long flightOrderRequestId = 10L;
			var defaultCreateFlightOrderRequest = GetDefaultCreateFlightOrderRequest();
			
			var mockFlightOrderDomainService = new Mock<FlightOrderDomainService>();
			mockFlightOrderDomainService.Setup(s => s.CreateFlightOrderRequest(userId,
				defaultCreateFlightOrderRequest.FlightNumber, defaultCreateFlightOrderRequest.Amount,
				defaultCreateFlightOrderRequest.DepartureDate)).Returns(new FlightOrderRequest
			{
				Id = flightOrderRequestId
			});
			
			var mockApprovalSystemProvider = new Mock<IApprovalSystemProvider>();
			var mockPaymentSystemProvider = new Mock<IPaymentSystemProvider>();
			
			var flightOrderService = new FlightOrderService(mockFlightOrderDomainService.Object, mockApprovalSystemProvider.Object, new InMemoryMessageSender(), mockPaymentSystemProvider.Object);
			flightOrderService.CreateFlightOrderRequest(userId, defaultCreateFlightOrderRequest);

			mockFlightOrderDomainService.Verify(s => s.CreateFlightOrderRequest(userId, defaultCreateFlightOrderRequest.FlightNumber, defaultCreateFlightOrderRequest.Amount, defaultCreateFlightOrderRequest.DepartureDate));
			mockApprovalSystemProvider.Verify(p => p.Approve(userId, It.Is<ApproveRequest>(r => 
				r.OrderRequestId == flightOrderRequestId &&
				r.Amount == defaultCreateFlightOrderRequest.Amount
			)));
		}
		
		[Fact]
		public void should_send_message_to_message_queue_when_confirm_flight_order()
		{
			var flightOrderRequest = Build(FlightOrderRequest())
				.With(r => r.Id = 10L)
				.Default();
			var flightOrder = Build(FlightOrder())
				.With(o => o.Id = 20L)
				.With(o => o.FlightOrderRequest = flightOrderRequest)
				.Default();

			var mockFlightOrderDomainService = new Mock<FlightOrderDomainService>();
			mockFlightOrderDomainService.Setup(s => s.ConfirmFlightOrderRequest(flightOrder.FlightOrderRequest.Id)).Returns(flightOrder);
			
			var mockApprovalSystemProvider = new Mock<IApprovalSystemProvider>();
			var mockPaymentSystemProvider = new Mock<IPaymentSystemProvider>();
			
			var inMemoryMessageSender = new InMemoryMessageSender();
			var flightOrderService = new FlightOrderService(mockFlightOrderDomainService.Object, mockApprovalSystemProvider.Object, inMemoryMessageSender, mockPaymentSystemProvider.Object);
			flightOrderService.ConfirmFlightOrderRequest(flightOrder.FlightOrderRequest.Id);

			var allSentMessages = inMemoryMessageSender.GetAllSentMessages();
			Assert.Single(allSentMessages);
			var flightOrderDto = JsonConvert.DeserializeObject<FlightOrderDto>(allSentMessages.Single());
			Assert.Equal(flightOrder.Id, flightOrderDto.Id);
			Assert.Equal(flightOrder.CreatedAt, flightOrderDto.CreatedAt);
			Assert.Equal(flightOrderRequest.Id, flightOrderDto.FlightOrderRequestId);
			Assert.Equal(flightOrderRequest.UserId, flightOrderDto.UserId);
			Assert.Equal(flightOrderRequest.Amount, flightOrderDto.Amount);
			Assert.Equal(flightOrderRequest.DepartureDate, flightOrderDto.DepartureDate);
			Assert.Equal(flightOrderRequest.FlightNumber, flightOrderDto.FlightNumber);
		}

		[Fact]
		public void should_call_domain_service_when_confirm_flight_order()
		{
			var flightOrderRequest = Build(FlightOrderRequest())
				.With(r => r.Id = 20L)
				.Default();
			var flightOrder = Build(FlightOrder())
				.With(o => o.FlightOrderRequest = flightOrderRequest)
				.Default();

			var mockFlightOrderDomainService = new Mock<FlightOrderDomainService>();
			mockFlightOrderDomainService.Setup(s => s.ConfirmFlightOrderRequest(flightOrder.FlightOrderRequest.Id)).Returns(flightOrder);
			
			var mockApprovalSystemProvider = new Mock<IApprovalSystemProvider>();
			var mockPaymentSystemProvider = new Mock<IPaymentSystemProvider>();
			
			var flightOrderService = new FlightOrderService(mockFlightOrderDomainService.Object, mockApprovalSystemProvider.Object, new InMemoryMessageSender(), mockPaymentSystemProvider.Object);
			flightOrderService.ConfirmFlightOrderRequest(flightOrder.FlightOrderRequest.Id);
			
			mockFlightOrderDomainService.Verify(s => s.ConfirmFlightOrderRequest(flightOrder.FlightOrderRequest.Id));
		}

		[Fact]
		public async Task should_call_payment_system_and_throw_ServiceUnavailableException_when_approval_system_is_unavailable()
		{
			const long userId = 1L;
			const long flightOrderRequestId = 10L;
			var defaultCreateFlightOrderRequest = GetDefaultCreateFlightOrderRequest();
			
			var mockFlightOrderDomainService = new Mock<FlightOrderDomainService>();
			mockFlightOrderDomainService.Setup(s => s.CreateFlightOrderRequest(userId,
				defaultCreateFlightOrderRequest.FlightNumber, defaultCreateFlightOrderRequest.Amount,
				defaultCreateFlightOrderRequest.DepartureDate)).Returns(new FlightOrderRequest
			{
				Id = flightOrderRequestId
			});
			
			var mockApprovalSystemProvider = new Mock<IApprovalSystemProvider>();
			mockApprovalSystemProvider.Setup(p => p.Approve(It.IsAny<long>(), It.IsAny<ApproveRequest>()))
				.Throws(new ServiceUnavailableException("error"));
			
			var mockPaymentSystemProvider = new Mock<IPaymentSystemProvider>();
			
			var flightOrderService = new FlightOrderService(mockFlightOrderDomainService.Object, mockApprovalSystemProvider.Object, new InMemoryMessageSender(), mockPaymentSystemProvider.Object);

			Task act() => flightOrderService.CreateFlightOrderRequest(userId, defaultCreateFlightOrderRequest);
			await Assert.ThrowsAsync<ServiceUnavailableException>(act);
			
			mockPaymentSystemProvider.Verify(p => p.PostponeServiceCharge());
		}
		
		private CreateFlightOrderRequest GetDefaultCreateFlightOrderRequest()
		{
			return new()
			{
				FlightNumber = "1001",
				Amount = 100,
				DepartureDate = DateTime.Parse("2021-10-30")
			};
		}
	}
}