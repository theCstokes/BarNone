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
    public class LiftAnalysisProfileController 
        : DefaultDetailController<LiftAnalysisProfileDTO, LiftAnalysisProfile, LiftAnalysisProfileRepository>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LiftController"/> class.
        /// </summary>
        public LiftAnalysisProfileController() : base((context) => new LiftAnalysisProfileRepository(context))
        {

        }
    }
}
