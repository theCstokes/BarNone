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
    /// <summary>
    /// Base controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    public class BaseController : Controller
    {
        /// <summary>
        /// Gets the filter request from the headers.
        /// </summary>
        /// <value>
        /// The filter request.
        /// </value>
        public FilterDTO FilterRequest
        {
            get
            {
                var filterHeader = Request.Headers["Filter"];
                if (filterHeader == StringValues.Empty) return null;
                return JsonConvert.DeserializeObject<FilterDTO>(filterHeader);
            }
        }

        /// <summary>
        /// Gets the user identifier from the token.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
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
