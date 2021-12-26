using System.Net.Http;
using System.Threading.Tasks;
using TravelManagement.Application.Providers;

namespace TravelManagement.Infrastructure.Providers
{
	public class PaymentSystemProvider : IPaymentSystemProvider
	{
		private readonly HttpClient _httpClient;

		public PaymentSystemProvider(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task PostponeServiceCharge()
		{
			var httpResponseMessage = await _httpClient.PostAsync("service-charges/postpone", null);
			httpResponseMessage.EnsureSuccessStatusCode();
		}
	}
}