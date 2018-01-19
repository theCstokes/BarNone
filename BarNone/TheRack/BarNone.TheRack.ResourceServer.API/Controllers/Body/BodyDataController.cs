using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
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
    /// <summary>
    /// Controller for the body data endpoints.
    /// </summary>
    /// <seealso cref="BarNone.TheRack.ResourceServer.API.Controllers.Core.DefaultDetailController{BarNone.Shared.DataTransfer.BodyDataDTO, BarNone.Shared.DomainModel.BodyData, BarNone.TheRack.Repository.BodyDataRepository}" />
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "User")]
    public class BodyDataController : DefaultDetailController<BodyDataDTO, BodyData, BodyDataRepository>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BodyDataController"/> class.
        /// </summary>
        public BodyDataController(): base((context) => new BodyDataRepository(context))
        {

        }
    }
}
