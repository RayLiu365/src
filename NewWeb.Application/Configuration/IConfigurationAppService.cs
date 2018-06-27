using System.Threading.Tasks;
using Abp.Application.Services;
using NewWeb.Configuration.Dto;

namespace NewWeb.Configuration
{
    public interface IConfigurationAppService: IApplicationService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}