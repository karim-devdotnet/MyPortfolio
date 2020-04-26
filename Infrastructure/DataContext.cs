using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Owner>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<PortfolioItem>().Property(x => x.Id).HasDefaultValueSql("NEWID()");

            //Db initializer
            modelBuilder.Entity<Owner>().HasData(
                new Owner 
                {
                    FullName = "Abdelkarim ABDALLAH" ,
                    Avatar = "avatar.bmp",
                    Profil = ".Net Fullstack Developer"
                });
        }

        public DbSet<Owner> OwnerSet { get; set; }
        public DbSet<PortfolioItem> PortfolioItemSet { get; set; }
    }
}
