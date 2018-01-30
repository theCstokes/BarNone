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
        /// <summary>
        /// Initializes a new instance of the <see cref="LiftRepository"/> class.
        /// </summary>
        public LiftRepository() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LiftRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public LiftRepository(DomainContext context) : base(context)
        {

        }

        /// <summary>
        /// Gets the data converter.
        /// </summary>
        /// <value>
        /// The data converter.
        /// </value>
        protected override ConverterResolverDelegate<Lift, LiftDTO> DataConverter => Converters.NewConvertion(context).Lift.CreateDataModel;

        /// <summary>
        /// Gets the set resolver.
        /// </summary>
        /// <value>
        /// The set resolver.
        /// </value>
        protected override SetResolverDelegate<Lift> SetResolver => (context) => context.Lifts;

        /// <summary>
        /// Gets the detail entity resolver.
        /// </summary>
        /// <value>
        /// The detail entity resolver.
        /// </value>
        protected override DetailResolverDelegate<Lift> DetailEntityResolver => (lifts) => lifts
                .Include(u => u.Parent)
                .Include(u => u.Video)
                .Include(u => u.BodyData).ThenInclude(d => d.BodyDataFrames).ThenInclude(f => f.Joints)
                .Include(u => u.BodyData).ThenInclude(d => d.BodyDataFrames).ThenInclude(f => f.Joints).ThenInclude(j => j.JointType)
                .Include(u => u.BodyData).ThenInclude(d => d.BodyDataFrames).ThenInclude(f => f.Joints).ThenInclude(j => j.JointTrackingStateType);

        /// <summary>
        /// Gets the entity resolver.
        /// </summary>
        /// <value>
        /// The entity resolver.
        /// </value>
        protected override EntityResolverDelegate<Lift> EntityResolver => (lifts) => lifts.Where(l => l.UserID == context.UserID);

        /// <summary>
        /// Creates the specified lift.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns></returns>
        public override Lift Create(LiftDTO dto)
        {
            var dm = DataConverter(dto);

            if (dm.Video != null)
            {
                dm.Video.Path = SaveVideo(dm.Video);
            }
            //dm.Video.UserID = 2;
            dm.VideoID = 2;
            var result = Create(dm);

            return result;
        }

        /// <summary>
        /// Saves the video.
        /// </summary>
        /// <param name="video">The video.</param>
        /// <returns></returns>
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
