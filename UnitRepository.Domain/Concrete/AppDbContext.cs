using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UnitRepository.Model.Core;

namespace UnitRepository.Domain.Concrete
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }

        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        DbSet<SampleClass> SampleClasses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Windows Authentication.
                optionsBuilder.UseSqlServer("Server=.\\INITIALIZE;Database=TestDb;Integrated Security=True;");

                // SQL Authentication.
                //optionsBuilder.UseSqlServer("Server=.\\INITIALIZE;Database=TruckDb;User Id=xxx;Password=xxx");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
