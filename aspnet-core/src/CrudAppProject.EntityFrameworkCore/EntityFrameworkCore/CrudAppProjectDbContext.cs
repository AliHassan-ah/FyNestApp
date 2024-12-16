using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using CrudAppProject.Authorization.Roles;
using CrudAppProject.Authorization.Users;
using CrudAppProject.MultiTenancy;
using CrudAppProject.Categories;
using CrudAppProject.Images;
using CrudAppProject.ProductDetails;
using CrudAppProject.Products;

namespace CrudAppProject.EntityFrameworkCore
{
    public class CrudAppProjectDbContext : AbpZeroDbContext<Tenant, Role, User, CrudAppProjectDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images {  get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }



        public CrudAppProjectDbContext(DbContextOptions<CrudAppProjectDbContext> options)
            : base(options)
        {
        }
    }
}
