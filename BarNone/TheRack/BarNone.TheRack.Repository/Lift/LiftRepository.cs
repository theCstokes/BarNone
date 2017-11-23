using BarNone.Shared.DataTransfer;
using BarNone.TheRack.DataAccess;
using BarNone.TheRack.DomainModel;
using BarNone.TheRack.Repository.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Xml.Linq;
using static BarNone.Shared.DataTransfer.Core.FilterDTO;

namespace BarNone.TheRack.Repository
{
    public class LiftRepository : BaseRepository<Lift, LiftDTO>
    {
        public LiftRepository() : base(new DomainContext())
        {
        }

        public LiftRepository(DomainContext context) : base(context)
        {

        }

        public override List<Lift> Get(WhereFunc where = null)
        {
            if (where != null)
            {
                return context.Lifts
                    .Where((l) => where(l))
                    .ToList();
            }
            return context.Lifts.ToList();
        }

        public override Lift Get(int id)
        {
            var result = context.Lifts.Where(c => c.ID == id).First();

            context.SaveChanges();

            return result;
        }

        public override Lift GetWithDetails(int id)
        {
            var result = context
                .Lifts
                .Include(u => u.Parent)
                .Where(c => c.ID == id).First();

            context.SaveChanges();

            return result;
        }

        public override Lift Create(LiftDTO dto)
        {
            //var entity = _adapter.GetDomainModel(dto);
            //var result = context.Lifts.Add(entity);

            //context.SaveChanges();

            //return result.Entity;
            return null;
        }

        public override Lift Update(int id, LiftDTO dto)
        {
            //var entity = _adapter.GetDomainModel(dto);
            //entity.ID = id;
            //var result = context.Lifts.Update(entity);

            //context.SaveChanges();

            //return result.Entity;
            return null;
        }

        public override Lift Remove(int id)
        {
            var result = context.Lifts.Remove(new Lift
            {
                ID = id
            });

            context.SaveChanges();

            return result.Entity;
        }
    }
}
