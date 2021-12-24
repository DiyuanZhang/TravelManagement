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
		}
	}
}