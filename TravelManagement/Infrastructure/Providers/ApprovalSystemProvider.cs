using System;
using System.Net.Http;
using System.Threading.Tasks;
using TravelManagement.Application.Dtos;
using TravelManagement.Application.Providers;
using TravelManagement.Infrastructure.Utils;

namespace TravelManagement.Infrastructure.Providers
{
	public class ApprovalSystemProvider : IApprovalSystemProvider
	{
		private readonly HttpClient _httpClient;

		public ApprovalSystemProvider(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task Approve(long userId, ApproveRequest request)
		{
			_httpClient.DefaultRequestHeaders.Add("UserId", userId.ToString());
			var httpResponseMessage = await _httpClient.PostAsync("approve", RequestUtils.ToHttpContent(request));
			httpResponseMessage.EnsureSuccessStatusCode();
		}
	}
}