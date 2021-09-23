using System;
using System.Threading.Tasks;
using MAWS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace MAWS.Tests
{
    public class TestBase
    {
        public bool IsSqlite = false;
        public TestBase()
        {
        }

        public async Task<ApplicationDbContext> GetDbContextAsync()
        {
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            if (IsSqlite)
            {
                // O o.. can't use sqlite:-> SQLite doesn't support computed column!
                //builder.EnableSensitiveDataLogging().UseSqlite("DataSource=:memory:", x=> { });
            }
            else
            {
                // Use In-Memory DB
                // In-Memory DB is not the best option as it is non relational,
                // for maintainers: consider having test database, simulating prod (like postgres)
                builder.UseInMemoryDatabase(Guid.NewGuid().ToString()).ConfigureWarnings(w =>
                {
                    w.Ignore(InMemoryEventId.TransactionIgnoredWarning);
                });
            }

            var dbContext = new ApplicationDbContext(builder.Options);

            if (IsSqlite)
            {
                // for sqlite
                //await dbContext.Database.OpenConnectionAsync();
            }

            await dbContext.Database.EnsureCreatedAsync();
            return dbContext;
        }
    }
}
