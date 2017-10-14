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

namespace TheRack.ResourceServer.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "User")]
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                UserRepository repository = new UserRepository();
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
                return EntityResponse.Entity(repository.Get(id));
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
                return EntityResponse.Entity(repository.GetWithDetails(id));
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
                return EntityResponse.Entity(repository.Create(value));
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
                return EntityResponse.Entity(repository.Update(id, value));
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
                return EntityResponse.Entity(repository.Remove(id));
            }
            catch (Exception e)
            {
                return EntityResponse.Error(e);
            }
        }
    }
}
