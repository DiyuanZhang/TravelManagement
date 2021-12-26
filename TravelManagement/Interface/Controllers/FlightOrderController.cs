using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelManagement.Application.Dtos;
using TravelManagement.Application.Services;
using TravelManagement.Infrastructure.Utils;
using TravelManagement.Interface.Validators;

namespace TravelManagement.Interface.Controllers
{
	[ApiController]
	public class FlightOrderController : ControllerBase
	{
		private readonly FlightOrderService _flightOrderService;
		private readonly CurrentUser _currentUser;
		private readonly ITimeProvider _timeProvider;
		
		public FlightOrderController(FlightOrderService flightOrderService, CurrentUser currentUser, ITimeProvider timeProvider)
		{
			_flightOrderService = flightOrderService;
			_currentUser = currentUser;
			_timeProvider = timeProvider;
		}
		
		[HttpPost("flight-orders")]
		public async Task<ActionResult<long>> CreateFlightOrderRequest(CreateFlightOrderRequest request)
		{
			var createFlightOrderRequestValidator = new CreateFlightOrderRequestValidator(_timeProvider);
			createFlightOrderRequestValidator.Validate(request);
			var flightOrderRequestId = await _flightOrderService.CreateFlightOrderRequest(_currentUser.UserId, request);
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