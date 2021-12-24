using System.Threading.Tasks;
using TravelManagement.Application.Dtos;
using TravelManagement.Infrastructure.Utils;
using TravelManagement.Test.ControllerFactsSetup;
using Xunit;

namespace TravelManagement.Test.ControllerFacts
{
    public class FlightOrderControllerFact : ControllerFactBase
    {
        [Fact]
        public async Task should_return_ok()
        {
            var httpContent = RequestUtils.ToHttpContent(new CreateFlightOrderRequest());
            var httpResponseMessage = await HttpClient.PostAsync("flight-orders", httpContent);
            httpResponseMessage.EnsureSuccessStatusCode();
        }
    }
}
