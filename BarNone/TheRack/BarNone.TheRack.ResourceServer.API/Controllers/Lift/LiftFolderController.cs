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
            var repo = new LiftFolderRepository();

            var filter = FilterRequest;
            if (filter != null)
            {
                return EntityResponse.Enumerable(repo.Get(filter.GetWhere()));
            }
            return EntityResponse.Enumerable(repo.Get());
        }

        public override IActionResult GetByID(int id)
        {
            var repo = new LiftFolderRepository();
            return EntityResponse.Entity<LiftFolder, LiftFolderDTO>(repo.Get(id));
        }

        public override IActionResult GetWithDetailsByID(int id)
        {
            var repo = new LiftFolderRepository();
            return EntityResponse
                .EntityDetail<LiftFolder, LiftFolderDTO, LiftFolderDetailDTO>(repo.GetWithDetails(id));
        }

        public override IActionResult Post([FromBody] LiftFolderDTO dto)
        {
            var repo = new LiftFolderRepository();
            return EntityResponse
                .EntityDetail<LiftFolder, LiftFolderDTO, LiftFolderDetailDTO>(repo.Create(dto));
        }

        public override IActionResult Put(int id, [FromBody] LiftFolderDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
