using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace CrudAppProject.Controllers
{
    public abstract class CrudAppProjectControllerBase: AbpController
    {
        protected CrudAppProjectControllerBase()
        {
            LocalizationSourceName = CrudAppProjectConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
