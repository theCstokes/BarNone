using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarNone.TheRack.DataAccess
{
    public class BaseDomainContext : DbContext
    {
        private static readonly string HOST = "127.0.0.1";
        private static readonly string PORT = "5432";
        private static readonly string USERNAME = "postgres";
        private static readonly string PASSWORD = "admin";
        private static readonly string DATABASE = "SharpSight";

        public BaseDomainContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Credentials);
        }

        public static string Credentials
        {
            get
            {
                return String.Format("User ID={0};Password={1};Host={2};Port={3};Database={4};",
                    USERNAME, PASSWORD, HOST, PORT, DATABASE);
            }
        }
    }
}
