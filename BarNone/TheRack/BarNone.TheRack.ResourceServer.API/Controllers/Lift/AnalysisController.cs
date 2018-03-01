using BarNone.Shared.DataTransfer.LiftData;
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

namespace BarNone.TheRack.ResourceServer.API.Controllers.Lift
{
    [Route("api/v1/[controller]")]
    [Authorize(Policy = "User")]
    public class AnalysisController : BaseController
    {
        [HttpGet("Lift/{id}")]
        public void GetAnalysis(int id, AnalysisRequestDTO requestDTO)
        {
            using (var context = new DomainContext(UserID))
            {
                using (var repo = new LiftRepository(context))
                {
                    var lift = repo.GetWithDetails(id);

                    requestDTO.Requests
                }
            }
        }
    }

    public class AnalysisRequestDTO
    {
        public List<AnalysisDTO> Requests { get; set; }
    }

    public class AnalysisDTO
    {
        public JointTypeDTO Target { get; set; }

        public JointTypeDTO Source { get; set; }

        [JsonConverter(typeof(EAnalysisTypeDTO))]
        public EAnalysisTypeDTO Type { get; set; }
    }

    public enum EAnalysisTypeDTO
    {
        Acceleration = 1,
        Angle = 2
    }

    public class LiftAnalysisPipeline
    {

    }

    public interface ILiftAnalysisPipe
    {
        bool ValidateRequest(AnalysisDTO analysis);
        void Execute();
    }

    public class LAP_Acceleration : ILiftAnalysisPipe
    {
        public void Execute()
        {
            throw new NotImplementedException();
        }

        public bool ValidateRequest(AnalysisDTO analysis)
        {
            if (analysis.Type != EAnalysisTypeDTO.Acceleration) return false;
            if (analysis.Target == null) return false;
            return true;
        }
    }

    public class LAP_Angle : ILiftAnalysisPipe
    {
        public void Execute()
        {
            throw new NotImplementedException();
        }

        public bool ValidateRequest(AnalysisDTO analysis)
        {
            if (analysis.Type != EAnalysisTypeDTO.Angle) return false;
            if (analysis.Target == null) return false;
            if (analysis.Source == null) return false;
            return true;
        }
    }
}
