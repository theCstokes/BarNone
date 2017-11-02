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

namespace BarNone.TheRack.ResourceServer.API.Controllers.Body
{
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "User")]
    public class BodyDataController : DetailController<BodyDataDTO>
    {
        public override IActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        public override IActionResult GetAll()
        {
            using (var repo = new BodyDataRepository())
            {
                return EntityResponse.Enumerable(repo.Get());
            }
        }

        public override IActionResult GetByID(int id)
        {
            using (var repo = new BodyDataRepository())
            {
                return EntityResponse.Entity<BodyData, BodyDataDTO>(repo.Get(id));
            }
        }

        public override IActionResult GetWithDetailsByID(int id)
        {
            throw new NotImplementedException();
        }

        public override IActionResult Post(BodyDataDTO dto)
        {
            using (var repo = new BodyDataRepository())
            {
                return EntityResponse.Entity<BodyData, BodyDataDTO>(repo.Create(dto));
            }
        }

        public override IActionResult Put(int id, BodyDataDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
