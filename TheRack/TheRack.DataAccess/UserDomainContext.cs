using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheRack.DomainModel;

namespace TheRack.DataAccess
{
    public class UserDomainContext : BaseDomainContext
    {
        public DbSet<User> User { get; set; }
    }
}
