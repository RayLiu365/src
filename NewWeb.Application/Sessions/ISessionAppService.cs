using System.Threading.Tasks;
using Abp.Application.Services;
using NewWeb.Sessions.Dto;

namespace NewWeb.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
