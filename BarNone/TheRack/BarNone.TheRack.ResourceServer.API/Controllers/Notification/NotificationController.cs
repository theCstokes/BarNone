using BarNone.TheRack.DataAccess;
using BarNone.TheRack.Repository;
using BarNone.TheRack.ResourceServer.API.Controllers.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheRack.ResourceServer.API.Response;

namespace BarNone.TheRack.ResourceServer.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "User")]
    public class NotificationController : BaseController
    {
        [HttpGet("Unread")]
        public IActionResult GetAllUnread()
        {
            using (var context = new DomainContext(UserID))
            {
                using (var repo = new NotificationRepository(context))
                {
                    return EntityResponse.Response(repo.GetUnread());
                }
            }
        }
    }
}
