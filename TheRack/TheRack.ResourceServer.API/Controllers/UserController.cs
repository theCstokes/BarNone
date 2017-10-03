using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TheRack.Repository;
using TheRack.DomainModel;
using System.Diagnostics;
using TheRack.ResourceServer.API.Response;
using TheRack.DataTransfer;

namespace TheRack.ResourceServer.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "User")]
    public class UserController : Controller
    {
        [HttpGet]
        public IResponse Users()
        {
            return EnumerableResponse.Create(UserRepository.Get());
        }

        [HttpGet("{id}")]
        public IResponse Get(int id)
        {
            return EntityResponse.Create(UserRepository.Get(id));
        }

        [HttpGet("{id}/Detail")]
        public IResponse GetWithDetails(int id)
        {
            return EntityResponse.Create(UserRepository.GetWithDetails(id));
        }

        [HttpPost]
        public IResponse Post([FromBody] UserDTO value)
        {
            return EntityResponse.Create(UserRepository.Create(value));
        }

        [HttpPut("{id}")]
        public IResponse Put(int id, [FromBody] UserDTO value)
        {
            return EntityResponse.Create(UserRepository.Update(id, value));
        }

        [HttpDelete("{id}")]
        public IResponse Delete(int id)
        {
            return EntityResponse.Create(UserRepository.Remove(id));
        }
    }
}
