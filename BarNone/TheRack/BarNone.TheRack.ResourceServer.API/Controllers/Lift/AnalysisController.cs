using BarNone.Shared.Analysis;
using BarNone.Shared.Analysis.LiftAnalysisPipeline.Acceleration;
using BarNone.Shared.Analysis.LiftAnalysisPipeline.Angle;
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
        [HttpGet("Lift/{id}")]
        public IActionResult GetAnalysis(int id)
        {
            using (var context = new DomainContext(UserID))
            {
                using (var repo = new LiftRepository(context))
                {
                    var lift = repo.GetWithDetails(id);

                    using (var analysisRepo = new LiftAnalysisProfileRepository(context))
                    {
                        var profile = analysisRepo.DetailEntities.Where(p => p.LiftType.ID == lift.LiftTypeID).FirstOrDefault();

                        var pipeline = profile.AngleAnalysisCriteria.Aggregate(new LiftAnalysisPipeline(), (result, obj) =>
                        {
                            result.Register(new LAP_Angle(new AR_Angle
                            {
                                Type = ELiftAnalysisType.Angle,
                                JointA = obj.JointTypeA,
                                JointB = obj.JointTypeB,
                                JointC = obj.JointTypeC
                            }, lift));
                            return result;
                        });

                        pipeline = profile.AccelerationAnalysisCriteria.Aggregate(pipeline, (result, obj) =>
                        {
                            result.Register(new LAP_Acceleration(new AR_Acceleration
                            {
                                Type = ELiftAnalysisType.Acceleration,
                                JointType = obj.JointType
                            }, lift));
                            return result;
                        });

                        pipeline = profile.PositionAnalysisCriteria.Aggregate(pipeline, (result, obj) =>
                        {
                            result.Register(new LAP_Position(new AR_Position
                            {
                                Type = ELiftAnalysisType.Position,
                                JointType = obj.JointType
                            }, lift));
                            return result;
                        });
                        pipeline = profile.SpeedAnalysisCriteria.Aggregate(pipeline, (result, obj) =>
                        {
                            result.Register(new LAP_Velocity(new AR_Velocity
                            {
                                Type = ELiftAnalysisType.Angle,
                                JointType = obj.JointType
                            }, lift));
                            return result;
                        });

                        // Execute.
                        return EntityResponse.Entity(pipeline.Execute());
                    }
                }
            }
        }

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

                        switch(request.Type)
                        {
                            case ELiftAnalysisType.Acceleration:
                                result.Register(new LAP_Acceleration(GetObject<AR_Acceleration>(obj), lift));
                                break;
                            case ELiftAnalysisType.Angle:
                                result.Register(new LAP_Angle(GetObject<AR_Angle>(obj), lift));
                                break;
                            case ELiftAnalysisType.Position:
                                result.Register(new LAP_Position(GetObject<AR_Position>(obj), lift));
                                break;
                            case ELiftAnalysisType.Velocity:
                                result.Register(new LAP_Velocity(GetObject<AR_Velocity>(obj), lift));
                                break;
                            default:
                                break;
                        }

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
