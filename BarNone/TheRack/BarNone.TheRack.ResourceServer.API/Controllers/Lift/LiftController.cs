﻿using System;
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

namespace TheRack.ResourceServer.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "User")]
    public class LiftController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                LiftRepository repository = new LiftRepository();
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
                LiftRepository repository = new LiftRepository();
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
                LiftRepository repository = new LiftRepository();
                var lift = repository.GetWithDetails(id);
                //var adapter = new LiftDataAdapter();
                //var dto = adapter.GetDTO(lift);
                return EntityResponse.Entity(lift);
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
                LiftRepository repository = new LiftRepository();
                return EntityResponse.Entity(repository.Create(value));
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
                LiftRepository repository = new LiftRepository();
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
                LiftRepository repository = new LiftRepository();
                return EntityResponse.Entity(repository.Remove(id));
            }
            catch (Exception e)
            {
                return EntityResponse.Error(e);
            }
        }
    }
}
