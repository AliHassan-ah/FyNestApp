using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace CrudAppProject.EntityFrameworkCore
{
    public static class CrudAppProjectDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<CrudAppProjectDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<CrudAppProjectDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
