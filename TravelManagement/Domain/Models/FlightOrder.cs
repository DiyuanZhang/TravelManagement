using System;

namespace TravelManagement.Domain.Models
{
	public class FlightOrder
	{
		public virtual long Id { get; set; }
		public virtual FlightOrderRequest FlightOrderRequest { get; set; }
		public virtual DateTime CreatedAt { get; set; }

		public virtual void Create()
		{
			CreatedAt = DateTime.UtcNow;
		}
	}
}