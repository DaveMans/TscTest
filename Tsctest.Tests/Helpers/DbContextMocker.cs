using Microsoft.EntityFrameworkCore;
using Tsctest.Dal;

namespace Tsctest.Tests.Helpers
{
    public class DbContextMocker
    {
        public static ApplicationDbContext GetWideWorldImportersDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var dbContext = new ApplicationDbContext(options);

            dbContext.Seed();

            return dbContext;
        }
    }
}
