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

namespace BarNone.TheRack.ResourceServer.API.Controllers.Lift
{
    /// <summary>
    /// LiftFolder endpoint controller.
    /// </summary>
    /// <seealso cref="BarNone.TheRack.ResourceServer.API.Controllers.Core.DefaultDetailController{BarNone.Shared.DataTransfer.LiftFolderDTO, BarNone.Shared.DomainModel.LiftFolder, BarNone.TheRack.Repository.LiftFolderRepository}" />
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "User")]
    public class LiftFolderController : DefaultDetailController<LiftFolderDTO, LiftFolder, LiftFolderRepository>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LiftFolderController"/> class.
        /// </summary>
        public LiftFolderController(): base((context) => new LiftFolderRepository(context))
        {

        }
    }
}
