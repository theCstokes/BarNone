using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using BarNone.TheRack.Repository.Body;
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
    /// Controller for joint endpoints.
    /// </summary>
    /// <seealso cref="BarNone.TheRack.ResourceServer.API.Controllers.Core.DefaultDetailController{BarNone.Shared.DataTransfer.JointDTO, BarNone.Shared.DomainModel.Joint, BarNone.TheRack.Repository.Body.JointRepository}" />
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "User")]
    public class JointController : DefaultDetailController<JointDTO, Joint, JointRepository>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JointController"/> class.
        /// </summary>
        public JointController(): base((context) => new JointRepository(context))
        {

        }
    }
}
