using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Infrastructure
{
    public class AuthenticationContext: DbContext
    {

        public AuthenticationContext(DbContextOptions options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

       
        public DbSet<TblUser> tblUsers { get; set; }
        public DbSet<TblAircrafts> tblAircrafts { get; set; }
        public DbSet<TblBookings> tblBookings { get; set; }
  
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TblUser>().ToTable("tblUsers");
            modelBuilder.Entity<TblAircrafts>().ToTable("tblAircrafts");
            modelBuilder.Entity<TblBookings>().ToTable("tblBookings");
       
        }
    }
}

   