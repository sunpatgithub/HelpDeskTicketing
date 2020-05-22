using HR.WebApi.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace HR.WebApi.ActionFilters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class TokenVerify : ActionFilterAttribute
    {
        private TokenService tokenService;

        public TokenVerify(TokenService tokenService)
        {
            this.tokenService = tokenService;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var vHeaders = filterContext.HttpContext.Request.Headers;

            if (String.IsNullOrEmpty(vHeaders["LOGIN_ID"]) && String.IsNullOrEmpty(Convert.ToString(vHeaders["TOKEN_NO"])))
            {
                filterContext.Result = new UnauthorizedResult(); return;
            }
            else
            {
                if (!tokenService.VerifyTokenByLoginId(Convert.ToString(vHeaders["LOGIN_ID"]), Convert.ToString(vHeaders["TOKEN_NO"])))
                    filterContext.Result = new UnauthorizedResult(); return;
            }
        }
    }
}
