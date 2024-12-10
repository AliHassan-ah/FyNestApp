using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CrudAppProject.EntityFrameworkCore;
using CrudAppProject.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace CrudAppProject.Web.Tests
{
    [DependsOn(
        typeof(CrudAppProjectWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class CrudAppProjectWebTestModule : AbpModule
    {
        public CrudAppProjectWebTestModule(CrudAppProjectEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CrudAppProjectWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(CrudAppProjectWebMvcModule).Assembly);
        }
    }
}