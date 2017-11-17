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
    public class BodyDataRepository : DefaultDetailRepository<BodyDataDTO, BodyData>
        //: BaseRepository<BodyDataDTO, BodyData>
    {
        public BodyDataRepository() : base(
            new DomainContext(),
            c => c.Bodies,
            //s => s.Include(b => b.BodyDataFrames.Select(f => f.Joints)))
            s => s.Include(b => b.BodyDataFrames).ThenInclude(l => l.Joints))
        {
        }

        public BodyDataRepository(DomainContext context) : base(
            context,
            c => c.Bodies,
            s => s.Include(b => b.BodyDataFrames).ThenInclude(l => l.Joints))
        {

        }

        //public override BodyData Create(BodyDataDTO dto)
        //{
        //    var bodyData = BodyData.CreateFromDTO(dto);
        //    var result = context.Bodies.Add(bodyData);

        //    context.SaveChanges();
        //    return result.Entity;
        //}

        //public override List<BodyData> Get(FilterDTO.WhereFunc where = null)
        //{
        //    if (where != null)
        //    {
        //        return context.Bodies
        //            .Where(b => where(b))
        //            .ToList();
        //    }
        //    return context.Bodies.ToList();
        //}

        //public override BodyData Get(int id)
        //{
        //    return context.Bodies.Where(b => b.ID == id).FirstOrDefault();
        //}

        //public override BodyData GetWithDetails(int id)
        //{
        //    return context.Bodies
        //        .Include(b => b.BodyDataFrames)
        //        .Where(b => b.ID == id)
        //        .FirstOrDefault();
        //}

        //public override BodyData Remove(int id)
        //{
        //    var bodyData = new BodyData
        //    {
        //        ID = id
        //    };

        //    var result = context.Remove(bodyData);
        //    return result.Entity;
        //}

        //public override BodyData Update(int id, BodyDataDTO dto)
        //{

        //    dto.ID = id;

        //    var bodyData = BodyData.CreateFromDTO(dto);
        //    var result = context.Bodies.Update(bodyData);

        //    context.SaveChanges();
        //    return result.Entity;
        //}
    }
}
