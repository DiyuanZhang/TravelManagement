using System.Threading.Tasks;

namespace TravelManagement.Application.Providers
{
	public interface IPaymentSystemProvider
	{
		Task PostponeServiceCharge();
	}
}