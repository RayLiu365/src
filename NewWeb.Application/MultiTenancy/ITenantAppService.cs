using Abp.Application.Services;
using Abp.Application.Services.Dto;
using NewWeb.MultiTenancy.Dto;

namespace NewWeb.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
