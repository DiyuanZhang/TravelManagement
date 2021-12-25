using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace TravelManagement.Interface.Filters
{
	public class AuthorizationFilter : IAuthorizationFilter
	{
		private const string UserIdKey = "UserId";
		
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			var headerDictionary = context.HttpContext.Request.Headers;
			if (!headerDictionary.ContainsKey(UserIdKey) || !long.TryParse(headerDictionary[UserIdKey].First(), out var userId))
			{
				context.Result = new UnauthorizedResult();
				return;
			}

			var currentUser = context.HttpContext.RequestServices.GetRequiredService<CurrentUser>();
			currentUser.UserId = userId;
		}
	}
}