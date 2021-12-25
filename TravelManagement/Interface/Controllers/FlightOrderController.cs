using Microsoft.AspNetCore.Mvc;
using TravelManagement.Application.Dtos;
using TravelManagement.Application.Services;
using TravelManagement.Interface.Filters;
using TravelManagement.Interface.Validators;

namespace TravelManagement.Interface.Controllers
{
	[ApiController]
	public class FlightOrderController : ControllerBase
	{
		private readonly FlightOrderService _flightOrderService;
		private readonly CurrentUser _currentUser;
		
		public FlightOrderController(FlightOrderService flightOrderService, CurrentUser currentUser)
		{
			_flightOrderService = flightOrderService;
			_currentUser = currentUser;
		}
		
		[HttpPost("flight-orders")]
		public ActionResult<long> CreateFlightOrderRequest(CreateFlightOrderRequest request)
		{
			CreateFlightOrderRequestValidator.Validate(request);
			var flightOrderRequestId = _flightOrderService.CreateFlightOrderRequest(_currentUser.UserId, request);
			return Ok(flightOrderRequestId);
		}
		
		[HttpPost("flight-orders/{oid}/confirmation")]
		public ActionResult<long> CreateFlightOrderRequest(long oid)
		{
			var flightOrderId = _flightOrderService.ConfirmFlightOrderRequest(oid);
			return Ok(flightOrderId);
		}
	}
}