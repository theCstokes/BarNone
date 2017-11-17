using BarNone.Shared.DataTransfer;
using BarNone.TheRack.DomainModel.Body;
using BarNone.TheRack.Repository;
using BarNone.TheRack.ResourceServer.API.Controllers.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarNone.TheRack.ResourceServer.API.Controllers.Body
{
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "User")]
    public class BodyDataFrameController : DefaultDetailController<BodyDataFrameDTO, BodyDataFrame, 
        BodyDataFrameRepository>
    {

        public BodyDataFrameController(): base(() => new BodyDataFrameRepository())
        {
        }
    }
}
