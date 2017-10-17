using BarNone.Shared.DataTransfer.Core;
using BarNone.TheRack.ResourceServer.API.Controllers.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarNone.TheRack.ResourceServer.API.Controllers
{
    public abstract class BasicController<TDTO> : BaseController
        where TDTO : BaseDTO<TDTO>, new()
    {
        [HttpGet]
        public abstract IActionResult GetAll();

        [HttpGet("{id}")]
        public abstract IActionResult GetByID(int id);

        [HttpPost]
        public abstract IActionResult Post(TDTO dto);

        [HttpPut("{id}")]
        public abstract IActionResult Put(int id, TDTO dto);

        [HttpDelete("{id}")]
        public abstract IActionResult Delete(int id);
    }
}
