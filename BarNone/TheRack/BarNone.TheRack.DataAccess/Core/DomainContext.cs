using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BarNone.TheRack.DataAccess
{
    public partial class DomainContext : BaseDomainContext
    {
        public delegate void ModelMapAction(ModelBuilder builder);

        private static event ModelMapAction OnModelMap;

        public DomainContext(int UserID = 0)
        {
            this.UserID = UserID;
        }

        protected static ModelMapAction CreateModelMapping(ModelMapAction action)
        {
            DomainContext.OnModelMap += action;
            return action;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelMap(modelBuilder);
        }

        public int UserID { get; set; }
    }
}
