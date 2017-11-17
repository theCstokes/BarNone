using BarNone.Shared.DTOTransformable.Core;
using BarNone.TheRack.DataAccess;
using BarNone.TheRack.FlexEngine;
using BarNone.TheRack.Repository;
using BarNone.TheRack.Repository.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheRack.ResourceServer.API.Response;

namespace BarNone.TheRack.ResourceServer.API.Controllers.Flex
{
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "User")]
    public class FlexController
    {
        [HttpPost]
        public IActionResult Flex([FromBody] FlexRequestDTO requestDTO)
        {
            var response = FlexRunner.Execute(requestDTO);

            return EntityResponse.Entity(response, System.Net.HttpStatusCode.OK);
        }
    }
}
