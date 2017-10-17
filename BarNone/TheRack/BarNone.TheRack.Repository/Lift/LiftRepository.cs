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
    public class LiftRepository : IRepository<LiftDTO, Lift>
    {

        public List<Lift> Get(WhereFunc where = null)
        {
            using (var context = new DomainContext())
            {
                if (where != null)
                {
                    return context.Lifts
                        .Where((l) => where(l))
                        .ToList();
                }
                return context.Lifts.ToList();
            }
        }

        public Lift Get(int id)
        {
            using (var context = new DomainContext())
            {
                var result = context.Lifts.Where(c => c.ID == id).First();

                context.SaveChanges();

                return result;
            }
        }

        public Lift GetWithDetails(int id)
        {
            using (var context = new DomainContext())
            {
                var result = context
                    .Lifts
                    .Include(u => u.Parent)
                    .Where(c => c.ID == id).First();

                context.SaveChanges();

                return result;
            }
        }

        public Lift Create(LiftDTO dto)
        {
            using (var context = new DomainContext())
            {
                //var entity = _adapter.GetDomainModel(dto);
                //var result = context.Lifts.Add(entity);

                //context.SaveChanges();

                //return result.Entity;
                return null;
            }
        }

        public Lift Update(int id, LiftDTO dto)
        {
            using (var context = new DomainContext())
            {
                //var entity = _adapter.GetDomainModel(dto);
                //entity.ID = id;
                //var result = context.Lifts.Update(entity);

                //context.SaveChanges();

                //return result.Entity;
                return null;
            }
        }

        public Lift Remove(int id)
        {
            using (var context = new DomainContext())
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
}
