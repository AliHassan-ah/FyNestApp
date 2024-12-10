using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using CrudAppProject.Configuration.Dto;

namespace CrudAppProject.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : CrudAppProjectAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
