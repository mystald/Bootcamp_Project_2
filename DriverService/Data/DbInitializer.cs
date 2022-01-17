using DriverService.Models;
using System;
using System.Linq;

namespace DriverService.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Drivers.Any())
            {
                return;
            }

            var drivers = new Driver[]
            {
                new Driver{FirstName ="Erick",LastName="Kurniawan",BirthDate=DateTime.Parse("2000-01-01"),Position="-6.193125, 106.821810",UserId=1},
                new Driver{FirstName ="Agus",LastName="Kurniawan",BirthDate=DateTime.Parse("2000-01-02"),Position="-7.193125, 106.821810",UserId=2},
                new Driver{FirstName ="Peter",LastName="Parker",BirthDate=DateTime.Parse("2000-01-03"),Position="-8.193125, 106.821810",UserId=3},
                new Driver{FirstName ="Tony",LastName="Stark",BirthDate=DateTime.Parse("2000-01-04"),Position="-9.193125, 106.821810",UserId=4},
                new Driver{FirstName ="Bruce",LastName="Banner",BirthDate=DateTime.Parse("2000-01-05"),Position="-10.193125, 106.821810",UserId=5}
            };

            foreach (var d in drivers)
            {
                context.Drivers.Add(d);
            }

            context.SaveChanges();
        }
    }
}
