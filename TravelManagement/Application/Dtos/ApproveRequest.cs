namespace TravelManagement.Application.Dtos
{
	public class ApproveRequest
	{
		public long OrderRequestId { get; set; }
		public decimal Amount { get; set; }
	}
}