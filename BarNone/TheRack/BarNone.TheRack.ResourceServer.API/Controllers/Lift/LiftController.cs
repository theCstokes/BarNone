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
using BarNone.TheRack.DomainModel;
using BarNone.TheRack.ResourceServer.API.Controllers;
using BarNone.TheRack.ResourceServer.API.Controllers.Core;

namespace TheRack.ResourceServer.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "User")]
    public class LiftController : BaseController
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                using (LiftRepository repository = new LiftRepository())
                {
                    var filter = FilterRequest;
                    if (filter != null)
                    {
                        return EntityResponse.Response(repository.Get(filter.GetWhere()));
                    }

                    return EntityResponse.Response(repository.Get());
                }
            }
            catch (Exception e)
            {
                return EntityResponse.Error(e);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                using (LiftRepository repository = new LiftRepository())
                {
                    return EntityResponse.Response(repository.Get(id));
                }
            }
            catch (Exception e)
            {
                return EntityResponse.Error(e);
            }
        }

        [HttpGet("{id}/Detail")]
        public IActionResult GetWithDetails(int id)
        {
            try
            {
                using (LiftRepository repository = new LiftRepository())
                {
                    var lift = repository.GetWithDetails(id);
                    return EntityResponse.DetailResponse(lift);
                }
            }
            catch (Exception e)
            {
                return EntityResponse.Error(e);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] LiftDTO value)
        {
            try
            {
                using (LiftRepository repository = new LiftRepository())
                {
                        return EntityResponse.Response(repository.Create(value));
                }
            }
            catch (Exception e)
            {
                return EntityResponse.Error(e);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] LiftDTO value)
        {
            try
            {
                using (LiftRepository repository = new LiftRepository())
                {
                    return EntityResponse.Response(repository.Update(id, value));
                }
            }
            catch (Exception e)
            {
                return EntityResponse.Error(e);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                using (LiftRepository repository = new LiftRepository())
                {
                    return EntityResponse.Response(repository.Remove(id));
                }
            }
            catch (Exception e)
            {
                return EntityResponse.Error(e);
            }
        }
    }
}
