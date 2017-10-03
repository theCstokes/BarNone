using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheRack.DomainModel;

namespace TheRack.DataAccess
{
    public class AccountDomainContext : BaseDomainContext
    {
        public DbSet<Account> User { get; set; }
    }
}
