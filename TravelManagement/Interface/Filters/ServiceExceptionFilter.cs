using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using TravelManagement.Application.Exceptions;
using TravelManagement.Interface.Exceptions;

namespace TravelManagement.Interface.Filters
{
	public class ServiceExceptionFilter : IAsyncExceptionFilter
	{
		private readonly ILogger<ServiceExceptionFilter> _logger;

		public ServiceExceptionFilter(ILogger<ServiceExceptionFilter> logger)
		{
			_logger = logger;
		}

		public Task OnExceptionAsync(ExceptionContext context)
		{
			_logger.LogError(context.Exception, "The error message: {Message}", context.Exception.Message);

			context.Result = context.Exception switch
			{
				BadRequestException exception => new BadRequestObjectResult(exception.Message),
				ServiceUnavailableException exception => new ObjectResult(exception.Message) { StatusCode = StatusCodes.Status503ServiceUnavailable},
				_ => new ObjectResult("An error occurred in travel management service") { StatusCode = StatusCodes.Status500InternalServerError }
			};

			context.ExceptionHandled = true;

			return Task.CompletedTask;
		}
	}
}