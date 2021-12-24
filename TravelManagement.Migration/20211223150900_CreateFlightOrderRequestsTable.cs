using FluentMigrator;

namespace TravelManagement.Migration
{
	[Migration(20211224150900)]
	public class CreateFlightOrderRequestsTable: FluentMigrator.Migration
	{
		private const string TABLE_NAME = "flight_order_requests";

		public override void Up()
		{
			Create.Table(TABLE_NAME)
				.WithColumn("id").AsInt64().PrimaryKey().Identity()
				.WithColumn("user_id").AsInt64().NotNullable()
				.WithColumn("flight_number").AsString().NotNullable()
				.WithColumn("amount").AsDecimal().NotNullable()
				.WithColumn("departure_date").AsDateTime()
				.WithColumn("created_at").AsDateTime().NotNullable()
				.WithColumn("expired_at").AsDateTime().NotNullable();
		}

		public override void Down()
		{
			Delete.Table(TABLE_NAME);
		}
	}
}