using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace TravelManagement.Infrastructure.Utils
{
	public static class RequestUtils
	{
		public static StringContent ToHttpContent(object? request)
		{
			var serializeRequest = JsonConvert.SerializeObject(request);
			return new StringContent(serializeRequest, Encoding.UTF8, "application/json");
		}
	}
}