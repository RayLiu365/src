using Abp;
using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using NewWeb.Authorization.Users;
using NewWeb.MultiTenancy;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewWeb
{
    public class MyLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        private readonly ILdapSettings _settings;
        private readonly IAbpZeroLdapModuleConfig _ldapModuleConfig;

        public MyLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
            _settings = settings;
            _ldapModuleConfig = ldapModuleConfig;
        }

        public async override Task<User> CreateUserAsync(string userNameOrEmailAddress, Tenant tenant)
        {
            await CheckIsEnabled(tenant);

            var user = await base.CreateUserAsync(userNameOrEmailAddress, tenant);

            using (var principalContext = await CreatePrincipalContext(tenant))
            {
                var userPrincipal = UserPrincipal.FindByIdentity(principalContext, userNameOrEmailAddress);
                
                if (userPrincipal == null)
                {
                    throw new AbpException("Unknown LDAP user: " + userNameOrEmailAddress);
                }

                UpdateUserFromPrincipal(user, userPrincipal);

                user.IsEmailConfirmed = true;
                user.IsActive = false;

                return user;
            }
        }

        public async override Task UpdateUserAsync(User user, Tenant tenant)
        {
            await CheckIsEnabled(tenant);

            await base.UpdateUserAsync(user, tenant);

            using (var principalContext = await CreatePrincipalContext(tenant))
            {
                var userPrincipal = UserPrincipal.FindByIdentity(principalContext, user.UserName);

                if (userPrincipal == null)
                {
                    throw new AbpException("Unknown LDAP user: " + user.UserName);
                }

                UpdateUserFromPrincipal(user, userPrincipal);
            }
        }

        protected override void UpdateUserFromPrincipal(User user, UserPrincipal userPrincipal)
        {
            user.UserName = userPrincipal.SamAccountName;
            user.Name = userPrincipal.GivenName;
            user.Surname = userPrincipal.Surname;
            user.EmailAddress = userPrincipal.EmailAddress;
        }

        private async Task CheckIsEnabled(Tenant tenant)
        {
            if (!_ldapModuleConfig.IsEnabled)
            {
                throw new AbpException("Ldap Authentication module is disabled globally!");
            }

            var tenantId = GetIdOrNull(tenant);
            if (!await _settings.GetIsEnabled(tenantId))
            {
                throw new AbpException("Ldap Authentication is disabled for given tenant (id:" + tenantId + ")! You can enable it by setting '" + LdapSettingNames.IsEnabled + "' to true");
            }
        }

        private static int? GetIdOrNull(Tenant tenant)
        {
            return tenant == null
                ? (int?)null
                : tenant.Id;
        }
        
    }
}
