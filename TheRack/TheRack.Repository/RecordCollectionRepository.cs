using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheRack.DataAccess;
using TheRack.DomainModel;

namespace TheRack.Repository
{
    public class RecordCollectionRepository
    {
        public List<RecordCollection> GetWithDetails()
        {
            using (var context = new DomainContext())
            {
                return context.RecordCollections
                    .Include(x => x.Records)
                    .ToList();
            }
        }
    }
}
