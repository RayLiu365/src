using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using NewWeb.Configuration.Dto;

namespace NewWeb.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : NewWebAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
