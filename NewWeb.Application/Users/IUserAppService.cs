using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using NewWeb.Roles.Dto;
using NewWeb.Users.Dto;

namespace NewWeb.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();
    }
}