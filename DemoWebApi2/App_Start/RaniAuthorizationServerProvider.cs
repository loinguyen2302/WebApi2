using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Owin.Security;

namespace DemoWebApi2
{
    public class RaniAuthorizationServerProvider: OAuthAuthorizationServerProvider
    {

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            string userName = context.UserName;
            string password = context.Password;
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            //
            //context.SetError("invalid_grant", "The user name or password is incorrect.");
            //return;
            var oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            oAuthIdentity.AddClaim(new Claim("username", context.UserName));
            //
            AuthenticationProperties properties = CreateProperties(userName);
            //
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);

            context.Validated(ticket);
            //if (userName == "admin" && password == "123456")
            //{
            //    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            //    identity.AddClaim(new Claim("username", context.UserName));

            //    context.Validated(identity);
            //}
            //else
            //{
            //    context.SetError("invalid_grant", "The user name or password is incorrect.");
            //    return;
            //}
            //return;


        }
        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "username", userName }
            };
            return new AuthenticationProperties(data);
        }
    }
}