using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TheRack.DomainModel;

namespace TheRack.DataAccess
{
    public partial class DomainContext : DbContext
    {
        private static readonly string HOST = "127.0.0.1";
        private static readonly string PORT = "5432";
        private static readonly string USERNAME = "postgres";
        private static readonly string PASSWORD = "admin";
        private static readonly string DATABASE = "SharpSight";

        public DomainContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Credentials);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<RecordCollection>()
                .HasMany(rc => rc.Records)
                .WithOne(r => r.RecordCollection);

            modelBuilder.Entity<Record>()
                .HasOne(r => r.RecordCollection)
                .WithMany(rc => rc.Records);

            //modelBuilder.Entity<RecordCollection>()
            //    .HasMany<Record>(s => s.Records);
            //.Map(cs =>
            //{
            //    cs.MapLeftKey("StudentRefId");
            //    cs.MapRightKey("CourseRefId");
            //    cs.ToTable("StudentCourse");
            //});

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
