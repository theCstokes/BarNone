using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BarNone.TheRack.ResourceServer.API.Controllers.Core;
using BarNone.Shared.DataTransfer;
using BarNone.TheRack.Repository;
using BarNone.Shared.DomainModel;
using Microsoft.AspNetCore.Authorization;
using BarNone.TheRack.DataAccess;
using TheRack.ResourceServer.API.Response;

namespace BarNone.TheRack.ResourceServer.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "User")]
    public class CommentController : DefaultDetailController<CommentDTO, Comment, CommmentRepository>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LiftController"/> class.
        /// </summary>
        public CommentController() : base((context) => new CommmentRepository(context))
        {

        }

        [HttpGet("Lift/{id}")]
        public IActionResult Get(int id)
        {
            using (var context = new DomainContext(UserID))
            {
                using (var repo = new CommmentRepository(context))
                {
                    var lift = repo.GetWithDetails(id);

                    return EntityResponse.Response(repo.GetLiftComments(id));
                }
            }
            //return null;
        }
    }
}