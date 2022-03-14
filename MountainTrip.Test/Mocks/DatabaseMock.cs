using Microsoft.EntityFrameworkCore;
using MountainTrip.Data;
using System;

namespace MountainTrip.Test.Mocks
{
    public static class DatabaseMock
    {
        public static MountainTripDbContext Instance
        {
            get 
            {
                var dbContextOptions = new DbContextOptionsBuilder<MountainTripDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new MountainTripDbContext(dbContextOptions);
            }
        }
    }
}
