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
using BarNone.Shared.DomainModel;
using BarNone.TheRack.ResourceServer.API.Controllers.Core;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using BarNone.TheRack.DataAccess;

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

        [HttpGet("{id}/Video")]
        public void Get(int id)
        {
            using (var context = new DomainContext(UserID))
            {
                using (var repo = new LiftRepository(context))
                {
                    var lift = repo.GetWithDetails(id);

                    HttpContext.Response.Headers.Add("Content-Disposition", $"attachment; filename={lift.Video.Path}");
                    var file = new FileInfo(lift.Video.Path);
                    //Check the file exist,  it will be written into the response 
                    if (file.Exists)
                    {
                        var stream = file.OpenRead();
                        var bytesinfile = new byte[stream.Length];
                        stream.Read(bytesinfile, 0, (int)file.Length);
                        HttpContext.Response.Body.Write(bytesinfile, 0, bytesinfile.Length);
                    }
                }
            }
            //return null;
        }
    }
}
