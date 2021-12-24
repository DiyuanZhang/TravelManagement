using System;
using Microsoft.AspNetCore.Mvc;
using TravelManagement.Application.Dtos;
using TravelManagement.Application.Providers;
using TravelManagement.Domain.Models;

namespace TravelManagement.Interface.Controllers
{
	[ApiController]
	public class FlightOrderController : ControllerBase
	{
		private readonly IMessageSender _messageSender;
		
		public FlightOrderController(IMessageSender messageSender)
		{
			_messageSender = messageSender;
		}
		
		[HttpPost("flight-orders")]
		public OkResult CreateFlightOrderRequest(CreateFlightOrderRequest request)
		{
			_messageSender.Send(new FlightOrderRequest
			{
				Amount = new decimal(123.45),
				DepartureDate = DateTime.UtcNow
			});
			return Ok();
		}
	}
}