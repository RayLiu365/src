using System.Threading.Tasks;
using Abp.Application.Services;
using NewWeb.Authorization.Accounts.Dto;

namespace NewWeb.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
