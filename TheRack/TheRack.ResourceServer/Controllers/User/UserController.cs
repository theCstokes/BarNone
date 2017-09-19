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

        //[HttpPost]
        //[Route("User")]
        //public IHttpActionResult Post([FromBody] UserDTO dto)
        //{

        //    var repo = new UserRepository();
        //    var user = repo.Create(dto);

        //    return new Response(HttpStatusCode.OK, user);
        //}

        //[HttpPut]
        //[Route("User")]
        //public IHttpActionResult Put([FromBody] UserDTO dto)
        //{

        //    var repo = new UserRepository();
        //    var user = repo.Update(dto);

        //    return new Response(HttpStatusCode.OK, user);
        //}
    }
}
