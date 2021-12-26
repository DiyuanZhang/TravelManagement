using TravelManagement.Application.Dtos;
using TravelManagement.Infrastructure.Utils;
using TravelManagement.Interface.Exceptions;

namespace TravelManagement.Interface.Validators
{
	public class CreateFlightOrderRequestValidator
	{
		private readonly ITimeProvider _timeProvider;
		
		public CreateFlightOrderRequestValidator(ITimeProvider timeProvider)
		{
			_timeProvider = timeProvider;
		}
		
		public void Validate(CreateFlightOrderRequest request)
		{
			if (string.IsNullOrWhiteSpace(request.FlightNumber))
			{
				throw new BadRequestException("Flight number can not be empty");
			}
			if (request.Amount < 0)
			{
				throw new BadRequestException("Flight prices cannot be negative");
			}
			if (request.DepartureDate <= _timeProvider.UtcNow())
			{
				throw new BadRequestException("Flight departure date must be later than current time");
			}
		}
	}
}