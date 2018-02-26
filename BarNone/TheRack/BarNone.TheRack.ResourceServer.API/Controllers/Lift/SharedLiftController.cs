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

namespace BarNone.TheRack.ResourceServer.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "User")]
    public class SharedLiftController : DefaultDetailController<LiftDTO, Lift, SharedLiftRepository>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LiftController"/> class.
        /// </summary>
        public SharedLiftController() : base((context) => new SharedLiftRepository(context))
        {

        }
    }
}
