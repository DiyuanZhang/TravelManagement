using FluentNHibernate.Mapping;
using TravelManagement.Domain.Models;

namespace TravelManagement.Infrastructure.Mappings
{
	public class FlightOrderRequestMapping : ClassMap<FlightOrderRequest>
	{
		public FlightOrderRequestMapping()
		{
			Table("flight_order_requests");
			Id(o => o.Id).GeneratedBy.Identity();
			Map(f => f.UserId).Column("user_id").Not.Nullable();
			Map(f => f.FlightNumber).Column("flight_number").Not.Nullable();
			Map(f => f.Amount).Column("amount").Not.Nullable();
			Map(f => f.DepartureDate).Column("departure_date").Not.Nullable();
			Map(f => f.CreatedAt).Column("created_at").Not.Nullable();
			Map(f => f.ExpiredAt).Column("expired_at").Not.Nullable();
		}
	}
}