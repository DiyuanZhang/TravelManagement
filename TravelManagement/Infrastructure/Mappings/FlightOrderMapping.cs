using FluentNHibernate.Mapping;
using TravelManagement.Domain.Models;

namespace TravelManagement.Infrastructure.Mappings
{
	public class FlightOrderMapping : ClassMap<FlightOrder>
	{
		public FlightOrderMapping()
		{
			Table("flight_orders");
			Id(p => p.Id).Column("id").GeneratedBy.Identity();
			References(p => p.FlightOrderRequest).Column("flight_order_request_id").Not.Nullable();
			Map(f => f.CreatedAt).Column("created_at").Not.Nullable();
		}
	}
}