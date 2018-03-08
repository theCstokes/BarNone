using BarNone.Shared.DataTransfer.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarNone.TheRack.ResourceServer.API.Controllers.Core
{
    public abstract class DetailController<TDTO> : BaseController
        where TDTO : BaseDTO<TDTO>, new()
    {
        [HttpGet]
        public abstract IActionResult GetAll();

        [HttpGet("{id}")]
        public abstract IActionResult GetByID(int id);

        [HttpGet("{id}/Details")]
        public abstract IActionResult GetWithDetailsByID(int id);

        [HttpPost]
        public abstract IActionResult Post([FromBody] TDTO dto);

        [HttpPut("{id}")]
        public abstract IActionResult Put(int id, [FromBody] TDTO dto);

        [HttpDelete("{id}")]
        public abstract IActionResult Delete(int id);
    }
}
