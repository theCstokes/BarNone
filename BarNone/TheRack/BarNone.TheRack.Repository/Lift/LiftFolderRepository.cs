using BarNone.Shared.DataTransfer;
using BarNone.Shared.DTOTransformable.Core;
using BarNone.TheRack.DataAccess;
using BarNone.TheRack.DataConverters;
using BarNone.TheRack.DomainModel;
using BarNone.TheRack.Repository.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static BarNone.Shared.DataTransfer.Core.FilterDTO;

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

        protected override ConverterResolver DetailDataConverterResolver => () => Converters.Convert.LiftFolder;

        protected override DbSetResolver SetResolver => (context) => context.LiftFolders;

        protected override Resolver DetailDataResolver => (s) => s.Include(l => l.Parent)
                .Include(l => l.SubFolders)
                .Include(l => l.Lifts);
    }

    //    : BaseRepository<LiftFolder, LiftFolderDTO>
    //{
    //    public LiftFolderRepository() : base(new DomainContext())
    //    {
    //    }

    //    public LiftFolderRepository(DomainContext context) : base(context)
    //    {

    //    }

    //    public override LiftFolder Create(LiftFolderDTO dto)
    //    {
    //        var folder = Converters.Convert.LiftFolder.CreateDataModel(dto);
    //        var result = context.LiftFolders.Add(folder);

    //        context.SaveChanges();
    //        return result.Entity;
    //    }

    //    public override List<LiftFolder> Get(WhereFunc where = null)
    //    {
    //        if (where != null)
    //        {
    //            return context.LiftFolders
    //                .Where((lf) => where(lf))
    //                .ToList();
    //        }
    //        return context.LiftFolders.ToList();
    //    }

    //    public override LiftFolder Get(int id)
    //    {
    //        return context.LiftFolders.Where(lf => lf.ID == id).First();
    //    }

    //    public override LiftFolder GetWithDetails(int id)
    //    {
    //        return context.LiftFolders
    //            .Include(lf => lf.Parent)
    //            .Include(lf => lf.Lifts)
    //            .Include(lf => lf.SubFolders)
    //            .Where(lf => lf.ID == id)
    //            .First();
    //    }

    //    public override LiftFolder Remove(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override LiftFolder Update(int id, LiftFolderDTO dto)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
