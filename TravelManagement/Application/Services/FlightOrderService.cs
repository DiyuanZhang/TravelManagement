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
			var flightOrderRequestId = _flightOrderDomainService.CreateFlightOrderRequest(userId, createFlightOrderRequest.FlightNumber,
				createFlightOrderRequest.Amount, createFlightOrderRequest.DepartureDate);
			_approvalSystemProvider.Approve(userId, new ApproveRequest
			{
				Amount = createFlightOrderRequest.Amount,
				OrderRequestId = flightOrderRequestId
			});
			return flightOrderRequestId;
		}
	}
}