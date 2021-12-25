using FluentMigrator;

namespace TravelManagement.Migration
{
	[Migration(20211225150900)]
	public class CreateFlightOrdersTable: FluentMigrator.Migration
	{
		private const string TABLE_NAME = "flight_orders";

		public override void Up()
		{
			Create.Table(TABLE_NAME)
				.WithColumn("id").AsInt64().PrimaryKey().Identity()
				.WithColumn("flight_order_request_id").AsInt64().NotNullable()
				.WithColumn("created_at").AsDateTime().NotNullable();
		}

		public override void Down()
		{
			Delete.Table(TABLE_NAME);
		}
	}
}