using BarNone.Shared.DataConverters;
using BarNone.Shared.DataTransfer;
using BarNone.TheRack.DataAccess;
using BarNone.Shared.DomainModel;
using BarNone.TheRack.Repository.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Xml.Linq;
using static BarNone.TheRack.Repository.Core.Resolvers;
using System.IO;

namespace BarNone.TheRack.Repository
{
    public class LiftRepository : DefaultDetailRepository<Lift, LiftDTO, LiftDetailDTO>
    {
        public LiftRepository() : base()
        {
        }

        public LiftRepository(DomainContext context) : base(context)
        {

        }

        protected override ConverterResolverDelegate<Lift, LiftDTO> DataConverter =>
            (dto) =>
            {
                var dm = Converters.Convert.Lift.CreateDataModel(dto);
                dm.UserID = context.UserID;
                return dm;
            };

        protected override SetResolverDelegate<Lift> SetResolver => (context) => context.Lifts;

        protected override DetailResolverDelegate<Lift> DetailEntityResolver => (lifts) => lifts
                .Include(u => u.Parent)
                .Include(u => u.Video)
                .Include(u => u.BodyData).ThenInclude(d => d.BodyDataFrames).ThenInclude(f => f.Joints)
                .Include(u => u.BodyData).ThenInclude(d => d.BodyDataFrames).ThenInclude(f => f.Joints).ThenInclude(j => j.JointType)
                .Include(u => u.BodyData).ThenInclude(d => d.BodyDataFrames).ThenInclude(f => f.Joints).ThenInclude(j => j.JointTrackingStateType);

        protected override EntityResolverDelegate<Lift> EntityResolver => (lifts) => lifts.Where(l => l.UserID == context.UserID);

        public override Lift Create(LiftDTO dto)
        {
            //return base.Create(dto);

            var dm = DataConverter(dto);
            var result = Create(dm);

            if (dm.Video != null)
            {
                SaveVideo(dm.Video);
            }

            return result;
        }

        private void SaveVideo(VideoRecord video)
        {
            var path = @"C:\VideoDB";
            string fullPath = "";
            do
            {
                var name = new Guid();
                fullPath = $"{path}\\{name}";
            } while (!File.Exists(fullPath));
            File.WriteAllBytes(fullPath, video.Data);
        }
    }
}
