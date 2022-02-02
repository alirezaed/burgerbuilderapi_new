using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using static BurgerBuilderApi.Tools.Helper;

namespace BurgerBuilderApi.Tools
{
	public class AuthAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(HttpActionContext actionContext)
		{
			SecurityHelper securityHelper = new SecurityHelper();
			string token = String.Empty;
			IEnumerable<string> authenticationHeaderValue;
			actionContext.Request.Headers.TryGetValues("Authorization", out authenticationHeaderValue);

			try
			{
				token = authenticationHeaderValue.FirstOrDefault().Split(' ')[1];
			}
			catch
			{
				token = String.Empty;
			}

			if (token == string.Empty)
			{
				actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, new { status = false, message = "authentication failed" });
				return;
			}
			else
			{
				bool isValidToken = securityHelper.ValidateToken(token);
				if (!isValidToken)
				{
					actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, new { status = false, message = "invalid token" });
					return;
				}
				else
				{
					base.OnActionExecuting(actionContext);
				}
			}
		}
	}
}
