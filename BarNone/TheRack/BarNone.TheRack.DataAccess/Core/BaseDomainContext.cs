using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;

namespace BarNone.TheRack.DataAccess
{
    /// <summary>
    /// Database interface credentials.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public class BaseDomainContext : DbContext
    {
        /// <summary>
        /// The host for db.
        /// </summary>
        private static readonly string HOST = "127.0.0.1";

        /// <summary>
        /// The port for db.
        /// </summary>
        private static readonly string PORT = "5433";

        /// <summary>
        /// The username for db.
        /// </summary>
        private static readonly string USERNAME = "postgres";

        /// <summary>
        /// The password for db.
        /// </summary>
        private static readonly string PASSWORD = "admin";

        /// <summary>
        /// The database name.
        /// </summary>
        private static readonly string DATABASE = "BarNone";

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
