using System.Collections.Generic;
using NewWeb.Roles.Dto;
using NewWeb.Users.Dto;

namespace NewWeb.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}