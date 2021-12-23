using FluentNHibernate.Mapping;
using TravelManagement.Domain.Models;

namespace TravelManagement.Infrastructure.Mappings
{
	public class OrderMapping : ClassMap<Order>
	{
		public OrderMapping()
		{
			Table("orders");
			Id(o => o.Id).GeneratedBy.Identity();
		}
	}
}