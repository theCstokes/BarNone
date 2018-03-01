using BarNone.Shared.Analysis;
using BarNone.Shared.Analysis.LiftAnalysisPipeline.Acceleration;
using BarNone.Shared.Analysis.LiftAnalysisPipeline.Core;
using BarNone.Shared.DataTransfer.LiftData;
using BarNone.Shared.DomainModel;
using BarNone.TheRack.DataAccess;
using BarNone.TheRack.Repository;
using BarNone.TheRack.ResourceServer.API.Controllers.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
                    var pipeline = requestDTO.Requests.Aggregate(new LiftAnalysisPipeline(), (result, request) =>
                    {
                        if (request.Type == ELiftAnalysisType.Acceleration)
                        {
                            result.Register(new LAP_Acceleration(lift, request));
                        }

                        return result;
                    });

                    // Execute.
                    return EntityResponse.Entity(pipeline.Execute());
                }
            }
        }
    }
}
