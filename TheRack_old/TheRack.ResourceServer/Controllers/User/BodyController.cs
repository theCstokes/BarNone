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
using Newtonsoft.Json.Linq;
using System.IO;
using System.Web;
using System.Text;
using Newtonsoft.Json;

namespace TheRack.ResourceServer.Controllers
{
    //[Authorize]
    [RoutePrefix("api/v1")]
    public class BodyController : ApiController
    {
        [HttpPost]
        [Route("Body")]
        public IHttpActionResult Post()
        {
            MemoryStream ms = new MemoryStream();
            HttpContext.Current.Request.InputStream.CopyTo(ms);
            byte[] dataArray = ms.ToArray();

            var strData = Encoding.Unicode.GetString(dataArray);
            dynamic body = JsonConvert.DeserializeObject(strData);

            Debug.WriteLine(dataArray);

            using (var dc = new DomainContext(RequestParser.GetParams(Request)))
            {
                var userRepo = new UserRepository();
                var userList = userRepo.Get();
                return new Response(HttpStatusCode.OK, userList);
            }
        }

        [HttpGet]
        [Route("Body")]
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
