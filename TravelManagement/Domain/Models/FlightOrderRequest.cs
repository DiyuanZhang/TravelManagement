using System;

namespace TravelManagement.Domain.Models
{
	public class FlightOrderRequest
	{
		public virtual long Id { get; set; }
		public virtual long UserId { get; set; }
		public virtual string FlightNumber { get; set; }
		public virtual decimal Amount { get; set; }
		public virtual DateTime DepartureDate { get; set; }
		public virtual DateTime CreatedAt { get; set; }
		public virtual DateTime ExpiredAt { get; set; }
		
		public virtual FlightOrder FlightOrder { get; set; }

		public virtual void Create()
		{
			CreatedAt = DateTime.UtcNow;
			ExpiredAt = DateTime.UtcNow.AddMinutes(30);
		}
	}
}