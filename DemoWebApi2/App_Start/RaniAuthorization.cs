using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace DemoWebApi2.App_Start
{
    public class RaniAuthorization:AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            return base.IsAuthorized(actionContext);
        }
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {

                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
                actionContext.Response.Content = new StringContent("NOT_ENOUGH_PERMISSION");
            }
            else
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                actionContext.Response.Content = new StringContent("NOT_AUTHENTICATED");
            }
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (SkipAuthorization(actionContext))
            {
                return;
            }
            var user = HttpContext.Current.User.Identity as System.Security.Principal.WindowsIdentity;
            //
            var currentuser = HttpContext.Current.User;
            if (user.IsAuthenticated)
            {
                
            }else
            {
                actionContext.Response = actionContext.ControllerContext.Request.CreateErrorResponse(
                HttpStatusCode.Unauthorized, "Session token expried or not valid.");
            }

        }
        
        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                   || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }
    }
}