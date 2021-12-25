using TravelManagement.Application.Dtos;
using TravelManagement.Application.Providers;
using TravelManagement.Domain.Services;

namespace TravelManagement.Application.Services
{
	public class FlightOrderService
	{
		private readonly FlightOrderDomainService _flightOrderDomainService;
		private readonly IApprovalSystemProvider _approvalSystemProvider;

		public FlightOrderService()
		{
			
		}
		
		public FlightOrderService(FlightOrderDomainService flightOrderDomainService, IApprovalSystemProvider approvalSystemProvider)
		{
			_flightOrderDomainService = flightOrderDomainService;
			_approvalSystemProvider = approvalSystemProvider;
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
			return flightOrder.Id;
		}
	}
}