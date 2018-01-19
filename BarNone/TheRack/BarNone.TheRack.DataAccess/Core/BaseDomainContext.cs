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

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDomainContext"/> class.
        /// </summary>
        public BaseDomainContext() : base()
        {
        }

        /// <summary>
        /// <para>
        /// Override this method to configure the database (and other options) to be used for this context.
        /// This method is called for each instance of the context that is created.
        /// </para>
        /// <para>
        /// In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have been passed
        /// to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
        /// the options have already been set, and skip some or all of the logic in
        /// <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
        /// </para>
        /// </summary>
        /// <param name="optionsBuilder">A builder used to create or modify options for this context. Databases (and other extensions)
        /// typically define extension methods on this object that allow you to configure the context.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Credentials);
        }

        /// <summary>
        /// Gets the credentials.
        /// </summary>
        /// <value>
        /// The credentials.
        /// </value>
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
