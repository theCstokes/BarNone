using BarNone.Shared.DataConverters;
using BarNone.Shared.DataTransfer;
using BarNone.Shared.DomainModel;
using BarNone.TheRack.DataAccess;
using BarNone.TheRack.Repository.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static BarNone.TheRack.Repository.Core.Resolvers;

namespace BarNone.TheRack.Repository
{
    public class LiftAnalysisProfileRepository
        : DefaultDetailRepository<LiftAnalysisProfile, LiftAnalysisProfileDTO, LiftAnalysisProfileDetailDTO>
    {
        public LiftAnalysisProfileRepository() : base()
        {
        }

        public LiftAnalysisProfileRepository(DomainContext context) : base(context)
        {
        }

        protected override ConverterResolverDelegate<LiftAnalysisProfile, LiftAnalysisProfileDTO> DataConverter =>
            Converters.NewConvertion(context).LiftAnalysisProfile.CreateDataModel;

        protected override SetResolverDelegate<LiftAnalysisProfile> SetResolver => (context) => context.LiftAnalysisProfiles;

        protected override EntityResolverDelegate<LiftAnalysisProfile> EntityResolver => (set) => set;

        protected override DetailResolverDelegate<LiftAnalysisProfile> DetailEntityResolver => (profile) => profile
            .Include(p => p.LiftType)
            .Include(p => p.AccelerationAnalysis).ThenInclude(a => a.JointType)
            .Include(p => p.PositionAnalysisCriteria).ThenInclude(a => a.JointType)
            .Include(p => p.SpeedAnalysisCriteria).ThenInclude(a => a.JointType)
            .Include(p => p.AngleAnalysisCriteria).ThenInclude(a => a.JointTypeA)
            .Include(p => p.AngleAnalysisCriteria).ThenInclude(a => a.JointTypeB)
            .Include(p => p.AngleAnalysisCriteria).ThenInclude(a => a.JointTypeC);
    }
}
