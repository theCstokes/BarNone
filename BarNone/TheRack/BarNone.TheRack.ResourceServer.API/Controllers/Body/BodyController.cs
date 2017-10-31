using BarNone.TheRack.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheRack.ResourceServer.API.Response;

namespace BarNone.TheRack.ResourceServer.API.Controllers.Body
{
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "User")]
    public class BodyController
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            using (var repo = new BodyRepository())
            {
                //var filter = FilterRequest;
                //if (filter != null)
                //{
                //    return EntityResponse.Enumerable(repo.Get(filter.GetWhere()));
                //}
                return EntityResponse.Enumerable(repo.Get());
            }
        }
    }
}
