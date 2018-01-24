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
    /// <summary>
    /// Lift endpoint controller.
    /// </summary>
    /// <seealso cref="BarNone.TheRack.ResourceServer.API.Controllers.Core.DefaultDetailController{BarNone.Shared.DataTransfer.LiftDTO, BarNone.Shared.DomainModel.Lift, BarNone.TheRack.Repository.LiftRepository}" />
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "User")]
    public class LiftController : DefaultDetailController<LiftDTO, Lift, LiftRepository>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LiftController"/> class.
        /// </summary>
        public LiftController() : base((context) => new LiftRepository(context))
        {

        }

        //[HttpPost]
        //public override IActionResult Post([FromBody] LiftDTO dto)
        //{
        //    if (dto == null)
        //    {
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            HttpContext.Request.Body.CopyTo(ms);
        //            byte[] data = ms.ToArray();
        //            var jsonString = Encoding.ASCII.GetString(data);
        //            dto = JsonConvert.DeserializeObject<LiftDTO>(jsonString);
        //        }
        //    }

        //    try
        //    {
        //        using (LiftRepository repository = new LiftRepository())
        //        {
        //            return EntityResponse.Response(repository.Create(dto));
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return EntityResponse.Error(e);
        //    }
        //}

        /// <summary>
        /// Gets video stream for the video linked with the lift from the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
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

        [HttpGet("EM")]
        public void EM()
        {
            using (var context = new DomainContext(UserID))
            {
                using (var repo = new LiftRepository(context))
                {
                    //var lift = repo.GetWithDetails(id);

                    var path = "C:\\VideoDB\\EM.mp4";

                    HttpContext.Response.Headers.Add("Content-Disposition", $"attachment; filename={path}");
                    var file = new FileInfo(path);
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
