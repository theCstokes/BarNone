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
    public class LiftFolderRepository : IRepository<LiftFolderDTO, LiftFolder>
    {
        public LiftFolder Create(LiftFolderDTO dto)
        {
            using (var dc = new DomainContext())
            {
                var folder = LiftFolder.CreateFromDTO(dto);
                var result = dc.LiftFolders.Add(folder);

                dc.SaveChanges();
                return result.Entity;
            }
        }

        public List<LiftFolder> Get(WhereFunc where = null)
        {
            using(var dc = new DomainContext())
            {
                if (where != null)
                {
                    return dc.LiftFolders
                        .Where((lf) => where(lf))
                        .ToList();
                }
                return dc.LiftFolders.ToList();
            }
        }

        public LiftFolder Get(int id)
        {
            using (var dc = new DomainContext())
            {
                return dc.LiftFolders.Where(lf => lf.ID == id).First();
            }
        }

        public LiftFolder GetWithDetails(int id)
        {
            using (var dc = new DomainContext())
            {
                return dc.LiftFolders
                    .Include(lf => lf.Parent)
                    .Include(lf => lf.Lifts)
                    .Include(lf => lf.SubFolders)
                    .Where(lf => lf.ID == id)
                    .First();
            }
        }

        public LiftFolder Remove(int id)
        {
            throw new NotImplementedException();
        }

        public LiftFolder Update(int id, LiftFolderDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
