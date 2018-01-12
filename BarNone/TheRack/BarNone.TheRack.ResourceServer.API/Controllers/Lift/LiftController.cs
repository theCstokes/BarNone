using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using TheRack.ResourceServer.API.Response;
using System.Net.Http;
using System.Net;
using BarNone.TheRack.Repository;
using BarNone.Shared.DataTransfer;
using BarNone.TheRack.DomainModel;
using BarNone.TheRack.ResourceServer.API.Controllers;
using BarNone.TheRack.ResourceServer.API.Controllers.Core;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace TheRack.ResourceServer.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "User")]
    public class LiftController : DefaultDetailController<LiftDTO, Lift, LiftRepository>
    {
        public LiftController() : base((context) => new LiftRepository(context))
        {

        }

        [HttpPost]
        public override IActionResult Post([FromBody] LiftDTO dto)
        {
            if (dto == null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    HttpContext.Request.Body.CopyTo(ms);
                    byte[] data = ms.ToArray();
                    var jsonString = Encoding.ASCII.GetString(data);
                    dto = JsonConvert.DeserializeObject<LiftDTO>(jsonString);
                }
            }

            try
            {
                using (LiftRepository repository = new LiftRepository())
                {
                    return EntityResponse.Response(repository.Create(dto));
                }
            }
            catch (Exception e)
            {
                return EntityResponse.Error(e);
            }
        }
    }
}
