using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UserManagement.Web.Filters
{
    public class AuthenticateFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Session.Get("UserDetails") == null)
            {
                context.Result = new RedirectResult("/Account/Login");
            }
        }
    }
}
