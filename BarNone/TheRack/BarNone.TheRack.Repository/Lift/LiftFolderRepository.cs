using BarNone.Shared.DataTransfer;
using BarNone.TheRack.DataAccess;
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
    public class LiftFolderRepository : BaseRepository<LiftFolderDTO, LiftFolder>
    {
        public LiftFolderRepository() : base(new DomainContext())
        {
        }

        public LiftFolderRepository(DomainContext context) : base(context)
        {

        }

        public override LiftFolder Create(LiftFolderDTO dto)
        {
            var folder = LiftFolder.CreateFromDTO(dto);
            var result = context.LiftFolders.Add(folder);

            context.SaveChanges();
            return result.Entity;
        }

        public override List<LiftFolder> Get(WhereFunc where = null)
        {
            if (where != null)
            {
                return context.LiftFolders
                    .Where((lf) => where(lf))
                    .ToList();
            }
            return context.LiftFolders.ToList();
        }

        public override LiftFolder Get(int id)
        {
            return context.LiftFolders.Where(lf => lf.ID == id).First();
        }

        public override LiftFolder GetWithDetails(int id)
        {
            return context.LiftFolders
                .Include(lf => lf.Parent)
                .Include(lf => lf.Lifts)
                .Include(lf => lf.SubFolders)
                .Where(lf => lf.ID == id)
                .First();
        }

        public override LiftFolder Remove(int id)
        {
            throw new NotImplementedException();
        }

        public override LiftFolder Update(int id, LiftFolderDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
