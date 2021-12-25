
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace TravelManagement.Application.Providers
{
	public interface IMessageSender
	{
		void Send(object messageBody);
		ConcurrentQueue<string> GetAllSentMessages();
	}
}