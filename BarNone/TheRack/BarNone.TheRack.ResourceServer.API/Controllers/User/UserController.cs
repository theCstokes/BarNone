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
using BarNone.Shared.DomainModel;
using BarNone.TheRack.ResourceServer.API.Controllers.Core;
using System.Security.Claims;

namespace TheRack.ResourceServer.API.Controllers
{
    /// <summary>
    /// User endpoint controller.
    /// </summary>
    /// <seealso cref="BarNone.TheRack.ResourceServer.API.Controllers.Core.BaseController" />
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "User")]
    public class UserController : BaseController
    {
        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                UserRepository repository = new UserRepository();

                var filter = FilterRequest;
                if (filter != null)
                {
                    return EntityResponse.Response(repository.Get(filter.GetWhere()));
                }
                return EntityResponse.Response(repository.Get());
            }
            catch(Exception e)
            {
                return EntityResponse.Error(e);
            }
        }

        /// <summary>
        /// Gets the user for the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                UserRepository repository = new UserRepository();
                return EntityResponse.Response(repository.Get(id));
            }
            catch (Exception e)
            {
                return EntityResponse.Error(e);
            }
        }

        //[HttpGet("{id}/Detail")]
        //public IActionResult GetWithDetails(int id)
        //{
        //    try
        //    {
        //        UserRepository repository = new UserRepository();
        //        return EntityResponse.Response(repository.GetWithDetails(id));
        //    }
        //    catch (Exception e)
        //    {
        //        return EntityResponse.Error(e);
        //    }
        //}

        /// <summary>
        /// Creates a user from the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] UserDTO value)
        {
            try
            {
                UserRepository repository = new UserRepository();
                return EntityResponse.Response(repository.Create(value));
            }
            catch (Exception e)
            {
                return EntityResponse.Error(e);
            }
        }

        /// <summary>
        /// Updates the user with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserDTO value)
        {
            try
            {
                UserRepository repository = new UserRepository();
                return EntityResponse.Response(repository.Update(id, value));
            }
            catch (Exception e)
            {
                return EntityResponse.Error(e);
            }
        }

        /// <summary>
        /// Deletes the user with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                UserRepository repository = new UserRepository();
                return EntityResponse.Response(repository.Remove(id));
            }
            catch (Exception e)
            {
                return EntityResponse.Error(e);
            }
        }
    }
}
