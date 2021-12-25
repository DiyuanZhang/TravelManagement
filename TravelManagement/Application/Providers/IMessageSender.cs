
using System.Collections.Concurrent;

namespace TravelManagement.Application.Providers
{
	public interface IMessageSender
	{
		void Send(object messageBody);
		ConcurrentQueue<string> GetAllSentMessages();
	}
}