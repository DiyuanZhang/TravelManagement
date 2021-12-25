using System.Collections.Concurrent;
using Newtonsoft.Json;
using TravelManagement.Application.Providers;

namespace TravelManagement.Test.ApplicationFactsSetup
{
	public class InMemoryMessageSender : IMessageSender
	{
		private readonly ConcurrentQueue<string> _queue;

		public InMemoryMessageSender()
		{
			_queue = new ConcurrentQueue<string>();
		}
		
		public void Send(object messageBody)
		{
			_queue.Enqueue(JsonConvert.SerializeObject(messageBody));
		}

		public ConcurrentQueue<string> GetAllSentMessages()
		{
			return _queue;
		}
	}
}