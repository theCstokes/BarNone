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

namespace BarNone.TheRack.ResourceServer.API.Controllers.Body
{
    /// <summary>
    /// Controller for body data frame endpoints.
    /// </summary>
    /// <seealso cref="BarNone.TheRack.ResourceServer.API.Controllers.Core.DefaultDetailController{BarNone.Shared.DataTransfer.BodyDataFrameDTO, BarNone.Shared.DomainModel.BodyDataFrame, BarNone.TheRack.Repository.BodyDataFrameRepository}" />
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "User")]
    public class BodyDataFrameController : DefaultDetailController<BodyDataFrameDTO, BodyDataFrame, 
        BodyDataFrameRepository>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BodyDataFrameController"/> class.
        /// </summary>
        public BodyDataFrameController(): base((context) => new BodyDataFrameRepository(context))
        {
        }
    }
}
