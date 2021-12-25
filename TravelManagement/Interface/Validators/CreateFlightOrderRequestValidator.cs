using System;
using TravelManagement.Application.Dtos;
using TravelManagement.Interface.Exceptions;

namespace TravelManagement.Interface.Validators
{
	public static class CreateFlightOrderRequestValidator
	{
		public static void Validate(CreateFlightOrderRequest request)
		{
			if (string.IsNullOrWhiteSpace(request.FlightNumber))
			{
				throw new BadRequestException("Flight number can not be empty");
			}
			if (request.Amount < 0)
			{
				throw new BadRequestException("Flight prices cannot be negative");
			}
			if (request.DepartureDate <= DateTime.UtcNow)
			{
				throw new BadRequestException("Flight departure date must be later than current time");
			}
		}
	}
}