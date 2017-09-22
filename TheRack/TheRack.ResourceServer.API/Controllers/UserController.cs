using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TheRack.Repository;
using TheRack.DomainModel;
using System.Diagnostics;

namespace TheRack.ResourceServer.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "User")]
    public class UserController : Controller
    {
        [HttpGet]
        public IEnumerable<User> Users()
        {
            var t = Stopwatch.StartNew();
            var result =  UserRepository.Get();
            Debug.WriteLine($"User Load Time {t.ElapsedMilliseconds}");
            return result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return UserRepository.Get(id);
        }

        // POST api/values
        [HttpPost]
        public User Post([FromBody] User value)
        {
            return UserRepository.Create(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public User Put(int id, [FromBody] User value)
        {
            return UserRepository.Update(id, value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public User Delete(int id)
        {
            return UserRepository.Remove(id);
        }
    }
}
