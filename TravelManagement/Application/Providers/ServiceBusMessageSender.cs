using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace TravelManagement.Application.Providers
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

		public async Task Send(object messageBody)
		{
			try
			{
				var serviceBusMessage = new ServiceBusMessage(JsonConvert.SerializeObject(messageBody));
				await _sender.SendMessageAsync(serviceBusMessage);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Fail to send messages to Azure Service Bus: {JsonConvert.SerializeObject(messageBody)}");
				throw;
			}
		}
	}
}