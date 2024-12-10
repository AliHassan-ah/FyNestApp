﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using CrudAppProject.Configuration;

namespace CrudAppProject.Web.Host.Startup
{
    [DependsOn(
       typeof(CrudAppProjectWebCoreModule))]
    public class CrudAppProjectWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public CrudAppProjectWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CrudAppProjectWebHostModule).GetAssembly());
        }
    }
}
