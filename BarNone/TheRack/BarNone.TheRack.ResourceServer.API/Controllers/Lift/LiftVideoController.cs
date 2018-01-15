using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BarNone.TheRack.ResourceServer.API.Controllers.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Internal;
using System.IO;

namespace BarNone.TheRack.ResourceServer.API.Controllers.Lift
{
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "User")]
    public class LiftVideoController : BaseController
    {
        [HttpGet]
        //[AllowAnonymous]
        public void Get()
        {
            //var videoFilePath = HostingEnvironment.MapPath("~/VideoFile/Win8.mp4");
            //The header information 

            HttpContext.Response.Headers.Add("Content-Disposition", "attachment; filename=Demo1.mp4");
            var file = new FileInfo(@"C:\VideoDB\Demo1.mp4");
            //Check the file exist,  it will be written into the response 
            if (file.Exists)
            {
                var stream = file.OpenRead();
                var bytesinfile = new byte[stream.Length];
                stream.Read(bytesinfile, 0, (int)file.Length);
                HttpContext.Response.Body.Write(bytesinfile, 0, bytesinfile.Length);
            }

            //var fileName = GetVideoFileName(id);
            //var video = new VideoStream(fileName);
            //var response = new HttpResponseMessage
            //{
            //    Content = new PushStreamContent(video.WriteToStream, new MediaTypeHeaderValue("video /mp4"))
            //};
            //var objectResult = new ObjectResult(response);
            //objectResult.ContentTypes.Add(new Microsoft.Net.Http.Headers.MediaTypeHeaderValue("video/mp4"));
            //return objectResult;
        }
    }
}