using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System.Net.Http;
using System.Net;
using BarNone.Shared.DataTransfer;
using BarNone.TheRack.Repository;
using TheRack.ResourceServer.API.Response;
using BarNone.TheRack.DomainModel;
using BarNone.TheRack.ResourceServer.API.Controllers.Core;

namespace TheRack.ResourceServer.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "User")]
    public class UserController : BaseController
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                UserRepository repository = new UserRepository();

                var filter = FilterRequest;
                if (filter != null)
                {
                    return EntityResponse.Enumerable(repository.Get(filter.GetWhere()));
                }
                return EntityResponse.Enumerable(repository.Get());
            }
            catch(Exception e)
            {
                return EntityResponse.Error(e);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                UserRepository repository = new UserRepository();
                return EntityResponse.Entity<User, UserDTO>(repository.Get(id));
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
                UserRepository repository = new UserRepository();
                return EntityResponse.Entity<User, UserDTO>(repository.GetWithDetails(id));
            }
            catch (Exception e)
            {
                return EntityResponse.Error(e);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserDTO value)
        {
            try
            {
                UserRepository repository = new UserRepository();
                return EntityResponse.Entity<User, UserDTO>(repository.Create(value));
            }
            catch (Exception e)
            {
                return EntityResponse.Error(e);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserDTO value)
        {
            try
            {
                UserRepository repository = new UserRepository();
                return EntityResponse.Entity<User, UserDTO>(repository.Update(id, value));
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
                UserRepository repository = new UserRepository();
                return EntityResponse.Entity<User, UserDTO>(repository.Remove(id));
            }
            catch (Exception e)
            {
                return EntityResponse.Error(e);
            }
        }
    }
}
