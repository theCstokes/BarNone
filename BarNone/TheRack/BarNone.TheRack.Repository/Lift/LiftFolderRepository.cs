using BarNone.Shared.DataTransfer;
using BarNone.TheRack.DataAccess;
using BarNone.Shared.DataConverters;
using BarNone.Shared.DomainModel;
using BarNone.TheRack.Repository.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static BarNone.TheRack.Repository.Core.Resolvers;

namespace BarNone.TheRack.Repository
{
    public class LiftFolderRepository : DefaultDetailRepository<LiftFolder, LiftFolderDTO, LiftFolderDetailDTO>
    {
        public LiftFolderRepository() : base()
        {

        }

        public LiftFolderRepository(DomainContext context) : base(context)
        {

        }

        //protected override DetailConverterResolverDelegate<LiftFolder, LiftFolderDTO, LiftFolderDetailDTO, Converters> DetailDataConverterResolver =>
        //    () => Converters.Convert.LiftFolder;

        protected override ConverterResolverDelegate<LiftFolder, LiftFolderDTO> DataConverter => 
            Converters.NewConvertion(context).LiftFolder.CreateDataModel;

        protected override DetailResolverDelegate<LiftFolder> DetailEntityResolver => (folders) => folders.Include(l => l.Parent)
                .Include(l => l.SubFolders)
                .Include(l => l.Lifts);

        protected override SetResolverDelegate<LiftFolder> SetResolver => (context) => context.LiftFolders;

        protected override EntityResolverDelegate<LiftFolder> EntityResolver => 
            (folders) => folders.Where(folder => folder.UserID == context.UserID);
    }
}
