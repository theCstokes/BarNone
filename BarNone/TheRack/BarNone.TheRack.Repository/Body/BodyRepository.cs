using BarNone.Shared.DataTransfer;
using BarNone.TheRack.DataAccess;
using BarNone.TheRack.DomainModel;
using BarNone.TheRack.Repository.Core;
using System;
using System.Collections.Generic;
using System.Text;
using BarNone.Shared.DataTransfer.Core;
using System.Linq;

namespace BarNone.TheRack.Repository
{
    public class BodyRepository : BaseRepository<BodyDataDTO, Body>
    {
        public BodyRepository() : base(new DomainContext())
        {
        }

        public BodyRepository(DomainContext context) : base(context)
        {

        }

        public override Body Create(BodyDataDTO dto)
        {
            throw new NotImplementedException();
        }

        public override List<Body> Get(FilterDTO.WhereFunc where = null)
        {
            return context.Bodies.ToList();
        }

        public override Body Get(int id)
        {
            throw new NotImplementedException();
        }

        public override Body GetWithDetails(int id)
        {
            throw new NotImplementedException();
        }

        public override Body Remove(int id)
        {
            throw new NotImplementedException();
        }

        public override Body Update(int id, BodyDataDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
