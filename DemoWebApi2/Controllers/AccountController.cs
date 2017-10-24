using Microsoft.Owin.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace DemoWebApi2.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("login")]
        public async Task<IHttpActionResult> Login()
        {
            //UserLoginModel model
            //if (model == null)
            //{
            //    return this.BadRequest("Invalid user data");
            //}
            UserLoginModel model = new DemoWebApi2.UserLoginModel()
            {
                Username = "admin",
                Password = "123456"
            };
            try
            {
                var server = TestServer.Create<Startup>();
                var requestParams = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", model.Username),
                    new KeyValuePair<string, string>("password", model.Password),
                 };
                var requestParamsFormUrlEncoded = new FormUrlEncodedContent(requestParams);
                var tokenServiceResponse = await server.HttpClient.PostAsync(Startup.TokenEndpointPath, requestParamsFormUrlEncoded);
                if (tokenServiceResponse.StatusCode == HttpStatusCode.OK)
                {
                    var userInfo = User;
                }
                return this.ResponseMessage(tokenServiceResponse);
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
        }
    }
}
