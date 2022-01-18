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
                new Driver{FirstName ="Erick",LastName="Kurniawan",BirthDate=DateTime.Parse("2001-01-01"),Latitude=-6.193125,Longitude=106.821810,Balance=100,UserId=1},
                new Driver{FirstName ="Agus",LastName="Kurniawan",BirthDate=DateTime.Parse("2002-01-02"),Latitude=-7.193125,Longitude=106.821810,Balance=100,UserId=2},
                new Driver{FirstName ="Peter",LastName="Parker",BirthDate=DateTime.Parse("2003-01-03"),Latitude=-8.193125,Longitude=106.821810,Balance=100,UserId=3},
                new Driver{FirstName ="Tony",LastName="Stark",BirthDate=DateTime.Parse("2004-01-04"),Latitude=-9.193125,Longitude=106.821810,Balance=100,UserId=4},
                new Driver{FirstName ="Bruce",LastName="Banner",BirthDate=DateTime.Parse("2005-01-05"),Latitude=-10.193125,Longitude=106.821810,Balance=100,UserId=5}
            };

            foreach (var d in drivers)
            {
                context.Drivers.Add(d);
            }

            context.SaveChanges();
        }
    }
}
