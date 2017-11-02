using BarNone.Shared.DataTransfer;
using BarNone.TheRack.DataAccess;
using BarNone.TheRack.DomainModel;
using BarNone.TheRack.Repository.Core;
using System;
using System.Collections.Generic;
using System.Text;
using BarNone.Shared.DataTransfer.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BarNone.TheRack.Repository
{
    public class BodyDataRepository : BaseRepository<BodyDataDTO, BodyData>
    {
        public BodyDataRepository() : base(new DomainContext())
        {
        }

        public BodyDataRepository(DomainContext context) : base(context)
        {

        }

        public override BodyData Create(BodyDataDTO dto)
        {
            var bodyData = BodyData.CreateFromDTO(dto);
            var result = context.Bodies.Add(bodyData);

            context.SaveChanges();
            return result.Entity;
        }

        public override List<BodyData> Get(FilterDTO.WhereFunc where = null)
        {
            if (where != null)
            {
                return context.Bodies
                    .Where(b => where(b))
                    .ToList();
            }
            return context.Bodies.ToList();
        }

        public override BodyData Get(int id)
        {
            return context.Bodies.Where(b => b.ID == id).FirstOrDefault();
        }

        public override BodyData GetWithDetails(int id)
        {
            return context.Bodies
                .Include(b => b.BodyDataFrames)
                .Where(b => b.ID == id)
                .FirstOrDefault();
        }

        public override BodyData Remove(int id)
        {
            throw new NotImplementedException();
        }

        public override BodyData Update(int id, BodyDataDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
