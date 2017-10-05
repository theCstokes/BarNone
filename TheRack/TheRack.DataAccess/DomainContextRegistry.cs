using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheRack.DomainModel;

namespace TheRack.DataAccess
{
    public partial class DomainContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<RecordCollection> RecordCollections { get; set; }

        public DbSet<Record> Records { get; set; }
    }
}
