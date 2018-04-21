using BarNone.Shared.Core;
using BarNone.Shared.DomainModel;
using BarNone.TheRack.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BarNone.TheRack.DataAccess
{
    /// <summary>
    /// Context for domain entites.
    /// </summary>
    /// <seealso cref="BarNone.TheRack.DataAccess.BaseDomainContext" />
    /// <seealso cref="BarNone.Shared.Core.IDomainContext" />
    class UserCredentialDomainContext : BaseDomainContext, IDomainContext
    {
        public int UserID { get; set; }

        public DbSet<UserCredential> UserCredentials { get; set; }

        public DbSet<User> Users{ get; set; }
    }
}
