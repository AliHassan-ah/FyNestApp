using Abp.Authorization;
using CrudAppProject.Authorization.Roles;
using CrudAppProject.Authorization.Users;

namespace CrudAppProject.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
