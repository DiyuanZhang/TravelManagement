using System;
using System.Collections.Concurrent;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TravelManagement.Application.Providers;

namespace TravelManagement.Infrastructure.Providers
{
	public class ServiceBusMessageSender : IMessageSender
	{
		private readonly ServiceBusSender _sender;
		private readonly ILogger<ServiceBusMessageSender> _logger;

		public ServiceBusMessageSender(ServiceBusSender sender, ILogger<ServiceBusMessageSender> logger)
		{
			_sender = sender;
			_logger = logger;
		}

		public async void Send(object messageBody)
		{
			try
			{
				var serviceBusMessage = new ServiceBusMessage(JsonConvert.SerializeObject(messageBody));
				_sender.SendMessageAsync(serviceBusMessage).GetAwaiter().GetResult();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Fail to send messages to Azure Service Bus: {JsonConvert.SerializeObject(messageBody)}");
				throw;
			}
		}

		public ConcurrentQueue<string> GetAllSentMessages()
		{
			throw new NotImplementedException();
		}
	}
}