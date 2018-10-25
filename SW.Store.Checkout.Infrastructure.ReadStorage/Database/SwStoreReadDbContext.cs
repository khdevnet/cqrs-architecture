using Microsoft.EntityFrameworkCore;

namespace Csrh.TimeReporting.Infrastructure.DataAccess.Database
{
    public class SwStoreReadDbContext : DbContext
    {
        public SwStoreReadDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
