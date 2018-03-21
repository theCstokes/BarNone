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
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

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

        /// <summary>
        /// Gets video stream for the video linked with the lift from the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpGet("{id}/Video")]
        public async Task GetOld(int id)
        {
            using (var context = new DomainContext(UserID))
            {
                using (var repo = new LiftRepository(context))
                {
                    var lift = repo.GetWithDetails(id);

                    //HttpContext.Response.Headers.Add("Content-Disposition", $"attachment; filename={lift.Video.Path}");
                    var file = new FileInfo(lift.Video.Path);
                    //Check the file exist,  it will be written into the response 
                    if (file.Exists)
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            using (FileStream fs = new FileStream(lift.Video.Path, FileMode.Open, FileAccess.Read))
                            {
                                byte[] bytes = new byte[fs.Length];
                                fs.Read(bytes, 0, (int)fs.Length);
                                ms.Write(bytes, 0, (int)fs.Length);

                                var r = new VideoStreamResult(ms, "video/avi");

                                await r.ExecuteResultAsync(ControllerContext);
                            }
                        }

                        //var stream = file.OpenRead();
                        //using (FileStream fs = new FileStream(file., FileMode.Open, FileAccess.Read))
                        //{
                        //    var r = new VideoStreamResult(stream, "video/mp4");

                        //    await r.ExecuteResultAsync(ControllerContext);
                        //}

                        //var bytesinfile = new byte[stream.Length];
                        //stream.Read(bytesinfile, 0, (int)file.Length);
                        //HttpContext.Response.Body.Write(bytesinfile, 0, bytesinfile.Length);
                    }
                    //return null;
                }
            }
            //return null;
        }

        [HttpGet("Fast")]
        public IActionResult Fast()
        {
            var path = "C:\\VideoDB\\EM.mp4";
            var file = new FileInfo(path);
            if (!file.Exists) return NotFound();
            var stream = file.OpenRead();

            //var outStream = new MemoryStream();
            return new FileStreamResult(stream, "video/mp4");
        }

        [HttpGet("{id}/Videof")]
        public IActionResult Get(int id)
        {
            using (var context = new DomainContext(UserID))
            {
                using (var repo = new LiftRepository(context))
                {
                    var lift = repo.GetWithDetails(id);

                    var path = "C:\\VideoDB\\EM.mp4";
                    var file = new FileInfo(lift.Video.Path);
                    if (!file.Exists) return NotFound();
                    using (var stream = file.OpenRead())
                    {
                        //var stream = _fileStorageClient.GetStream(container, name); //Got from storage
                        if (stream == null)
                            return NotFound();

                        Response.Headers["Accept-Ranges"] = "bytes";

                        //if there is no range - this is usual request
                        var rangeHeaderValue = Request.Headers["Range"].FirstOrDefault();
                        if (string.IsNullOrEmpty(rangeHeaderValue))
                        {
                            var fileStreamResult = new FileStreamResult(stream, "video/mp4");
                            Response.ContentLength = stream.Length;
                            Response.StatusCode = (int)HttpStatusCode.OK;
                            return fileStreamResult;
                        }

                        if (!TryReadRangeItem(rangeHeaderValue, stream.Length, out long start, out long end))
                        {
                            return StatusCode((int)HttpStatusCode.RequestedRangeNotSatisfiable);
                        }

                        Response.Headers["Content-Range"] = $"{start}-{end}/{stream.Length}";
                        Response.ContentLength = end - start + 1;
                        Response.StatusCode = (int)HttpStatusCode.PartialContent;

                        using (var outStream = new MemoryStream())
                        {
                            CreatePartialContent(stream, outStream, start, end);
                            outStream.Seek(0, SeekOrigin.Begin);
                            return new FileStreamResult(outStream, "video/mp4");
                        }
                    }
                }
            }
        }

        private static void CreatePartialContent(Stream inputStream, Stream outputStream,
            long start, long end)
        {
            var ReadStreamBufferSize = 64 * 1024;
            var remainingBytes = end - start + 1;
            var buffer = new byte[ReadStreamBufferSize];
            long position;
            inputStream.Position = start;
            do
            {
                try
                {
                    var count = remainingBytes > ReadStreamBufferSize ?
                        inputStream.Read(buffer, 0, ReadStreamBufferSize) :
                        inputStream.Read(buffer, 0, (int)remainingBytes);
                    outputStream.Write(buffer, 0, count);
                }
                catch (Exception error)
                {
                    Debug.WriteLine(error);
                    break;
                }
                position = inputStream.Position;
                remainingBytes = end - position + 1;
            } while (position <= end);
        }

        private bool TryReadRangeItem(string rangeHeaderValue, long contentLength,
            out long start, out long end)
        {
            if (string.IsNullOrEmpty(rangeHeaderValue))
                throw new ArgumentNullException(nameof(rangeHeaderValue));

            start = 0;
            end = contentLength - 1;

            var rangeHeaderSplitted = rangeHeaderValue.Split('=');
            if (rangeHeaderSplitted.Length == 2)
            {
                var range = rangeHeaderSplitted[1].Split('-');
                if (range.Length == 2)
                {
                    if (long.TryParse(range[0], out long startParsed))
                        start = startParsed;
                    if (long.TryParse(range[1], out long endParsed))
                        end = endParsed;
                }
            }

            return start < contentLength && end < contentLength;
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

    public class VideoStreamResult : FileStreamResult
    {
        // default buffer size as defined in BufferedStream type
        private const int BufferSize = 0x1000;
        private string MultipartBoundary = "<qwe123>";

        public VideoStreamResult(Stream fileStream, string contentType)
            : base(fileStream, contentType)
        {

        }

        public VideoStreamResult(Stream fileStream, MediaTypeHeaderValue contentType)
            : base(fileStream, "video/mp4")
        {

        }

        private bool IsMultipartRequest(RangeHeaderValue range)
        {
            return range != null && range.Ranges != null && range.Ranges.Count > 1;
        }

        private bool IsRangeRequest(RangeHeaderValue range)
        {
            return range != null && range.Ranges != null && range.Ranges.Count > 0;
        }

        protected async Task WriteVideoAsync(HttpResponse response)
        {
            var bufferingFeature = response.HttpContext.Features.Get<IHttpBufferingFeature>();
            bufferingFeature?.DisableResponseBuffering();

            var length = FileStream.Length;

            var range = response.HttpContext.GetRanges(length);

            if (IsMultipartRequest(range))
            {
                response.ContentType = $"multipart/byteranges; boundary={MultipartBoundary}";
            }
            else
            {
                response.ContentType = ContentType.ToString();
            }

            response.Headers.Add("Accept-Ranges", "bytes");

            if (IsRangeRequest(range))
            {
                response.StatusCode = (int)HttpStatusCode.PartialContent;

                if (!IsMultipartRequest(range))
                {
                    response.Headers.Add("Content-Range", $"bytes {range.Ranges.First().From}-{range.Ranges.First().To}/{length}");
                }

                foreach (var rangeValue in range.Ranges)
                {
                    if (IsMultipartRequest(range)) // dunno if multipart works
                    {
                        await response.WriteAsync($"--{MultipartBoundary}");
                        await response.WriteAsync(Environment.NewLine);
                        await response.WriteAsync($"Content-type: {ContentType}");
                        await response.WriteAsync(Environment.NewLine);
                        await response.WriteAsync($"Content-Range: bytes {range.Ranges.First().From}-{range.Ranges.First().To}/{length}");
                        await response.WriteAsync(Environment.NewLine);
                    }

                    await WriteDataToResponseBody(rangeValue, response);

                    if (IsMultipartRequest(range))
                    {
                        await response.WriteAsync(Environment.NewLine);
                    }
                }

                if (IsMultipartRequest(range))
                {
                    await response.WriteAsync($"--{MultipartBoundary}--");
                    await response.WriteAsync(Environment.NewLine);
                }
            }
            else
            {
                await FileStream.CopyToAsync(response.Body);
            }
        }

        private async Task WriteDataToResponseBody(RangeItemHeaderValue rangeValue, HttpResponse response)
        {
            var startIndex = rangeValue.From ?? 0;
            var endIndex = rangeValue.To ?? 0;

            byte[] buffer = new byte[BufferSize];
            long totalToSend = endIndex - startIndex;
            int count = 0;

            long bytesRemaining = totalToSend + 1;
            response.ContentLength = bytesRemaining;

            FileStream.Seek(startIndex, SeekOrigin.Begin);

            while (bytesRemaining > 0)
            {
                try
                {
                    if (bytesRemaining <= buffer.Length)
                        count = FileStream.Read(buffer, 0, (int)bytesRemaining);
                    else
                        count = FileStream.Read(buffer, 0, buffer.Length);

                    if (count == 0)
                        return;

                    await response.Body.WriteAsync(buffer, 0, count);

                    bytesRemaining -= count;
                }
                catch (IndexOutOfRangeException)
                {
                    await response.Body.FlushAsync();
                    return;
                }
                finally
                {
                    await response.Body.FlushAsync();
                }
            }
        }

        public override async Task ExecuteResultAsync(ActionContext context)
        {
            await WriteVideoAsync(context.HttpContext.Response);
        }
    }

    public static class Ex
    {
        public static RangeHeaderValue GetRanges(this HttpContext context, long contentSize)
        {
            RangeHeaderValue rangesResult = null;

            string rangeHeader = context.Request.Headers["Range"];

            if (!string.IsNullOrEmpty(rangeHeader))
            {
                // rangeHeader contains the value of the Range HTTP Header and can have values like:
                //      Range: bytes=0-1            * Get bytes 0 and 1, inclusive
                //      Range: bytes=0-500          * Get bytes 0 to 500 (the first 501 bytes), inclusive
                //      Range: bytes=400-1000       * Get bytes 500 to 1000 (501 bytes in total), inclusive
                //      Range: bytes=-200           * Get the last 200 bytes
                //      Range: bytes=500-           * Get all bytes from byte 500 to the end
                //
                // Can also have multiple ranges delimited by commas, as in:
                //      Range: bytes=0-500,600-1000 * Get bytes 0-500 (the first 501 bytes), inclusive plus bytes 600-1000 (401 bytes) inclusive

                // Remove "Ranges" and break up the ranges
                string[] ranges = rangeHeader.Replace("bytes=", string.Empty).Split(",".ToCharArray());

                rangesResult = new RangeHeaderValue();

                for (int i = 0; i < ranges.Length; i++)
                {
                    const int START = 0, END = 1;

                    long endByte, startByte;

                    long parsedValue;

                    string[] currentRange = ranges[i].Split("-".ToCharArray());

                    if (long.TryParse(currentRange[END], out parsedValue))
                        endByte = parsedValue;
                    else
                        endByte = contentSize - 1;


                    if (long.TryParse(currentRange[START], out parsedValue))
                        startByte = parsedValue;
                    else
                    {
                        // No beginning specified, get last n bytes of file
                        // We already parsed end, so subtract from total and
                        // make end the actual size of the file
                        startByte = contentSize - endByte;
                        endByte = contentSize - 1;
                    }

                    rangesResult.Ranges.Add(new RangeItemHeaderValue(startByte, endByte));
                }
            }

            return rangesResult;
        }
    }
}
