using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace TravelManagement.Interface.Filters
{
	public class AuthorizationFilter : IAuthorizationFilter
	{
		public const string USER_ID_KEY = "UserId";
		
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			var headerDictionary = context.HttpContext.Request.Headers;
			if (!headerDictionary.ContainsKey(USER_ID_KEY) || !long.TryParse(headerDictionary[USER_ID_KEY].First(), out var userId))
			{
				context.Result = new UnauthorizedResult();
				return;
			}

			var currentUser = context.HttpContext.RequestServices.GetRequiredService<CurrentUser>();
			currentUser.UserId = userId;
		}
	}
}