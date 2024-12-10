using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using CrudAppProject.Authorization.Roles;
using CrudAppProject.Authorization.Users;
using CrudAppProject.MultiTenancy;

namespace CrudAppProject.EntityFrameworkCore
{
    public class CrudAppProjectDbContext : AbpZeroDbContext<Tenant, Role, User, CrudAppProjectDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public CrudAppProjectDbContext(DbContextOptions<CrudAppProjectDbContext> options)
            : base(options)
        {
        }
    }
}
