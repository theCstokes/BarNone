using BarNone.Shared.DataTransfer.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BarNone.TheRack.ResourceServer.API.Controllers.Core
{
    public class BaseController : Controller
    {
        public FilterDTO FilterRequest
        {
            get
            {
                var filterHeader = Request.Headers["Filter"];
                if (filterHeader == StringValues.Empty) return null;
                return JsonConvert.DeserializeObject<FilterDTO>(filterHeader);
            }
        }

        public int UserID
        {
            get
            {
                var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
                var userIDClaim = claimsIdentity.FindFirst("UserID");
                return Convert.ToInt32(userIDClaim.Value);
            }
        }
    }
}
