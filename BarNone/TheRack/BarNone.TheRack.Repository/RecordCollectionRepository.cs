using BarNone.TheRack.DataAccess;
using BarNone.TheRack.DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarNone.TheRack.Repository
{
    public class RecordCollectionRepository
    {
        public List<RecordCollection> GetWithDetails()
        {
            using (var context = new DomainContext())
            {
                //return context.RecordCollections
                //    .Include(x => x.Records)
                //    .ToList();
                return null;
            }
        }
    }
}
