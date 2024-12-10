using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CrudAppProject.Authorization;

namespace CrudAppProject
{
    [DependsOn(
        typeof(CrudAppProjectCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class CrudAppProjectApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<CrudAppProjectAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(CrudAppProjectApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
