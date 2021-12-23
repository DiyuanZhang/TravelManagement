using FluentMigrator;

namespace TravelManagement.Migration
{
	[Migration(20211223150900)]
	public class CreateOrdersTable: FluentMigrator.Migration
	{
		private const string TABLE_NAME = "orders";

		public override void Up()
		{
			Create.Table(TABLE_NAME)
				.WithColumn("id").AsInt64().PrimaryKey().Identity();
		}

		public override void Down()
		{
			Delete.Table(TABLE_NAME);
		}
	}
}