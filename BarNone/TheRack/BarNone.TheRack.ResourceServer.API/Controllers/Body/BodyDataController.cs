using BarNone.Shared.DataTransfer;
using BarNone.Shared.DTOTransformable.Core;
using BarNone.TheRack.DomainModel;
using BarNone.TheRack.Repository;
using BarNone.TheRack.ResourceServer.API.Controllers.Core;
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
    public class BodyDataController : DefaultDetailController<BodyDataDTO, BodyData, BodyDataRepository>
    {

        public BodyDataController(): base(
            () => new BodyDataRepository(),
            () => new ConvertConfig(4))
        {

        }
    }
}
