using BarNone.Shared.DataTransfer;
using BarNone.Shared.DTOTransformable.Core;
using BarNone.TheRack.DataAccess;
using BarNone.TheRack.FlexEngine;
using BarNone.TheRack.Repository;
using BarNone.TheRack.Repository.Core;
using BarNone.TheRack.ResourceServer.API.Controllers.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRack.ResourceServer.API.Response;

namespace BarNone.TheRack.ResourceServer.API.Controllers.Flex
{
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "User")]
    public class FlexController : BaseController
    {
        [HttpPost]
        public IActionResult Flex()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                HttpContext.Request.Body.CopyTo(ms);
                byte[] data = ms.ToArray();
                var jsonString = Encoding.ASCII.GetString(data);
                var dto = JsonConvert.DeserializeObject<LiftDTO>(jsonString);

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
}
