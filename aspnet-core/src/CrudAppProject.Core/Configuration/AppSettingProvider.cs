using System.Collections.Generic;
using Abp.Configuration;

namespace CrudAppProject.Configuration
{
    public class AppSettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new[]
            {
                new SettingDefinition(AppSettingNames.UiTheme, "red", scopes: SettingScopes.Application | SettingScopes.Tenant | SettingScopes.User, clientVisibilityProvider: new VisibleSettingClientVisibilityProvider()),
                new SettingDefinition(AppSettingNames.DefaultSenderEmail, "alihassan.ah3535@gmail.com", scopes: SettingScopes.Application | SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.SMTPHost, "smtp.ethereal.email", scopes: SettingScopes.Application | SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.SMTPPort, "587", scopes: SettingScopes.Application | SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.UserName, "armani.macgyver@ethereal.email", scopes: SettingScopes.Application | SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.Password, "wNNBq1GZEynyXnXXKj", scopes: SettingScopes.Application | SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.TestEmail, "", scopes: SettingScopes.Application | SettingScopes.Tenant),
            };
        }
    }
}
