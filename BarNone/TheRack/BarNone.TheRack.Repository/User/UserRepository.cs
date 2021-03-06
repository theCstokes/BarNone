﻿using BarNone.Shared.DataTransfer;
using BarNone.TheRack.DataAccess;
using BarNone.Shared.DataConverters;
using BarNone.Shared.DomainModel;
using BarNone.TheRack.Repository.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using static BarNone.TheRack.Repository.Core.Resolvers;

namespace BarNone.TheRack.Repository
{
    public class UserRepository : DefaultRepository<User, UserDTO>
    {
        protected override ConverterResolverDelegate<User, UserDTO> DataConverter => Converters.NewConvertion(context).User.CreateDataModel;

        protected override SetResolverDelegate<User> SetResolver => (context) => context.Users;

        protected override EntityResolverDelegate<User> EntityResolver => (set) => set;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        public UserRepository() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UserRepository(DomainContext context) : base(context)
        {
        }
    }
}
