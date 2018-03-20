﻿using BarNone.Shared.Analysis;
using BarNone.Shared.Analysis.LiftAnalysisPipeline.Acceleration;
using BarNone.Shared.Analysis.LiftAnalysisPipeline.Core;
using BarNone.Shared.Analysis.LiftAnalysisPipeline.Position;
using BarNone.Shared.Analysis.LiftAnalysisPipeline.Velocity;
using BarNone.Shared.DataTransfer.LiftData;
using BarNone.Shared.DomainModel;
using BarNone.TheRack.DataAccess;
using BarNone.TheRack.Repository;
using BarNone.TheRack.ResourceServer.API.Controllers.Core;
using BarNone.TheRack.ResourceServer.API.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheRack.ResourceServer.API.Response;

namespace BarNone.TheRack.ResourceServer.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "User")]
    public class AnalysisController : BaseController
    {
        [HttpPost("Lift/{id}")]
        public IActionResult GetAnalysis(int id, [FromBody] AnalysisRequest requestDTO)
        {
            using (var context = new DomainContext(UserID))
            {
                using (var repo = new LiftRepository(context))
                {
                    var lift = repo.GetWithDetails(id);

                    // Create Pipeline.
                    var pipeline = requestDTO.Requests.Aggregate(new LiftAnalysisPipeline(), (result, obj) =>
                    {
                        var request = GetObject<RequestEntity>(obj);
                        if (request.Type == ELiftAnalysisType.Position)
                            result.Register(new LAP_Position(GetObject<AR_Position>(obj), lift));
                        else if (request.Type == ELiftAnalysisType.Velocity)
                            result.Register(new LAP_Velocity(GetObject<AR_Velocity>(obj), lift));

                        return result;
                    });

                    // Execute.
                    return EntityResponse.Entity(pipeline.Execute());
                }
            }
        }

        private TSource GetObject<TSource>(object obj)
            where TSource : class
        {
            return ((JObject)obj).ToObject(typeof(TSource), JsonSerializer.Create(new JsonSerializerSettings
            {
                ContractResolver = new JsonPropertiesResolver()
            })) as TSource;
        }
    }
}
