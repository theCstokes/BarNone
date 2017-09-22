using TheRack.DataAccess;
using TheRack.DataTransfer.UserDTO;
using TheRack.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Diagnostics;

namespace TheRack.ResourceServer.Controllers
{
    [Authorize]
    [RoutePrefix("api/v1")]
    public class UserController : ApiController
    {
        [HttpGet]
        [Route("User")]
        public IHttpActionResult Get()
        {
            using (var dc = new DomainContext(RequestParser.GetParams(Request)))
            {
                var userRepo = new UserRepository();
                var userList = userRepo.Get();
                return new Response(HttpStatusCode.OK, userList);
            }
        }
    }
}
