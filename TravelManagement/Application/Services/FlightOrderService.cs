using TravelManagement.Application.Dtos;
using TravelManagement.Application.Providers;
using TravelManagement.Domain.Services;

namespace TravelManagement.Application.Services
{
	public class FlightOrderService
	{
		private readonly FlightOrderDomainService _flightOrderDomainService;
		private readonly IApprovalSystemProvider _approvalSystemProvider;
		private readonly IMessageSender _messageSender;

		public FlightOrderService()
		{
		}
		
		public FlightOrderService(FlightOrderDomainService flightOrderDomainService, IApprovalSystemProvider approvalSystemProvider, IMessageSender messageSender)
		{
			_flightOrderDomainService = flightOrderDomainService;
			_approvalSystemProvider = approvalSystemProvider;
			_messageSender = messageSender;
		}

		public virtual long CreateFlightOrderRequest(long userId, CreateFlightOrderRequest createFlightOrderRequest)
		{
			var flightOrderRequest = _flightOrderDomainService.CreateFlightOrderRequest(userId, createFlightOrderRequest.FlightNumber,
				createFlightOrderRequest.Amount, createFlightOrderRequest.DepartureDate);
			_approvalSystemProvider.Approve(userId, new ApproveRequest
			{
				Amount = createFlightOrderRequest.Amount,
				OrderRequestId = flightOrderRequest.Id
			});
			return flightOrderRequest.Id;
		}
		
		public virtual long ConfirmFlightOrderRequest(long flightOrderRequestId)
		{
			var flightOrder = _flightOrderDomainService.ConfirmFlightOrderRequest(flightOrderRequestId);
			_messageSender.Send(FlightOrderDto.Build(flightOrder));
			return flightOrder.Id;
		}
	}
}