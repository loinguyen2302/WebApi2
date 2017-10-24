using DemoWebApi2.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DemoWebApi2.Controllers
{
    public class HomeController : BaseApiController
    {
        [RaniAuthorization]
        //[Authorize]
        [HttpGet]
        public IHttpActionResult Authen()
        {
            return Ok();
        }
        [AllowAnonymous]
        public IHttpActionResult Test()
        {
            return Ok();
        }
    }
}
