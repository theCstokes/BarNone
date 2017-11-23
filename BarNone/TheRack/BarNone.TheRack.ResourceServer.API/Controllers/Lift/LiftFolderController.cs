using BarNone.Shared.DataTransfer;
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

namespace BarNone.TheRack.ResourceServer.API.Controllers.Lift
{
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "User")]
    public class LiftFolderController : DetailController<LiftFolderDTO>
    {
        public override IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override IActionResult GetAll()
        {
            using (var repo = new LiftFolderRepository())
            {
                var filter = FilterRequest;
                if (filter != null)
                {
                    return EntityResponse.Enumerable(repo.Get(filter.GetWhere()));
                }
                return EntityResponse.Enumerable(repo.Get());
            }
        }

        public override IActionResult GetByID(int id)
        {
            using (var repo = new LiftFolderRepository())
            {
                return EntityResponse.Response(repo.Get(id));
            }
        }

        public override IActionResult GetWithDetailsByID(int id)
        {
            using (var repo = new LiftFolderRepository())
            {
                return EntityResponse
                    .DetailResponse(repo.GetWithDetails(id));
            }
        }

        public override IActionResult Post([FromBody] LiftFolderDTO dto)
        {
            using (var repo = new LiftFolderRepository())
            {
                return EntityResponse
                    .Response(repo.Create(dto));
            }
        }

        public override IActionResult Put(int id, [FromBody] LiftFolderDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
