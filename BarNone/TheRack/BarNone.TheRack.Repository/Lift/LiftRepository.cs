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

        protected override ConverterResolverDelegate<Lift, LiftDTO> DataConverter => Converters.NewConvertion(context).Lift.CreateDataModel;

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

            if (dm.Video != null)
            {
                dm.Video.Path = SaveVideo(dm.Video);
            }
            dm.Video.UserID = 2;
            var result = Create(dm);

            return result;
        }

        private string SaveVideo(VideoRecord video)
        {
            var path = @"C:\VideoDB";
            string fullPath = "";
            do
            {
                var name = Guid.NewGuid();
                fullPath = $"{path}\\{name}.mp4";
            } while (File.Exists(fullPath));
            File.WriteAllBytes(fullPath, video.Data);
            return fullPath;
        }
    }
}
