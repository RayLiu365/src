using Abp.Authorization;
using NewWeb.Authorization.Roles;
using NewWeb.Authorization.Users;

namespace NewWeb.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
