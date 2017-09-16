using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace TheRack.AuthorizationServer.Controllers
{
    [RoutePrefix("api/v1/authorize")]
    public class AuthorizeController : ApiController
    {

        [Route("Login")]
        public IHttpActionResult Login()
        {
            return null;
        }
    }
}