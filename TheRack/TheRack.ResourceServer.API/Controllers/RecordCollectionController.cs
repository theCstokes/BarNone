using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheRack.Repository;
using TheRack.ResourceServer.API.Response;

namespace TheRack.ResourceServer.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "User")]
    public class RecordCollectionController : Controller
    {
        [HttpGet("{Detail}")]
        public IResponse GetWithDetails()
        {
            var repo = new RecordCollectionRepository();
            return EntityResponse.Create(repo.GetWithDetails());
        }
    }
}
