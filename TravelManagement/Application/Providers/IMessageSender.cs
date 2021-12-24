using System.Threading.Tasks;

namespace TravelManagement.Application.Providers
{
	public interface IMessageSender
	{
		Task Send(object messageBody);
	}
}