using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Contexts
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfIntrest> PointOfIntrest { get; set; }

        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("connection string");
        //    base.OnConfiguring(optionsBuilder);
        //}

        //Added Seed data
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<City>().HasData(
        //            new City()
        //            {
        //                Id = 1,
        //                Name = "Denver",
        //                Description = "The city that is a mile high"
        //            },
        //            new City()
        //            {
        //                Id = 2,
        //                Name = "Parker",
        //                Description = "The city that is really a town"
        //            },
        //            new City()
        //            {
        //                Id = 3,
        //                Name = "Westminster",
        //                Description = "it west of Denver"
        //            }
        //        );
        //    modelBuilder.Entity<PointOfIntrest>().HasData(
        //            new PointOfIntrest()
        //            {
        //                Id = 1,
        //                CityId = 1,
        //                Name = "Denver Mint",
        //                Description = "The place where money is made."
        //            },
        //            new PointOfIntrest()
        //            {
        //                Id = 2,
        //                CityId = 1,
        //                Name = "Denver Mint",
        //                Description = "The place where money is made."
        //            },
        //            new PointOfIntrest()
        //            {
        //                Id = 3,
        //                CityId = 2,
        //                Name = "O'Brian Water Park",
        //                Description = "The water park in Parker"
        //            },
        //            new PointOfIntrest()
        //            {
        //                Id = 4,
        //                CityId = 2,
        //                Name = "Endevor Park",
        //                Description = "A made up park name"
        //            },
        //            new PointOfIntrest()
        //            {
        //                Id = 5,
        //                CityId = 3,
        //                Name = "Westminster High School",
        //                Description = "A high school in the city."
        //            },
        //            new PointOfIntrest()
        //            {
        //                Id = 6,
        //                CityId = 3,
        //                Name = "Westminster Park",
        //                Description = "A made up park name"
        //            }
        //        );
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
