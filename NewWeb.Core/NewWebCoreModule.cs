using System.Reflection;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Zero;
using Abp.Zero.Configuration;
using Abp.Zero.Ldap;
using Abp.Zero.Ldap.Configuration;
using NewWeb.Authorization;
using NewWeb.Authorization.Roles;
using NewWeb.Authorization.Users;
using NewWeb.Configuration;
using NewWeb.MultiTenancy;

namespace NewWeb
{
    [DependsOn(typeof(AbpZeroCoreModule),typeof(AbpZeroLdapModule))]
    public class NewWebCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            IocManager.Register<ILdapSettings, MyLdapSettings>(); //change default setting source
            Configuration.Modules.ZeroLdap().Enable(typeof(MyLdapAuthenticationSource));
            Configuration.Modules.Zero().UserManagement.ExternalAuthenticationSources.Add<MyExternalAuthSource>();

            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            //Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);
            

            //Remove the following line to disable multi-tenancy.
            Configuration.MultiTenancy.IsEnabled = NewWebConsts.MultiTenancyEnabled;

            //Add/remove localization sources here
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    NewWebConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(),
                        "NewWeb.Localization.Source"
                        )
                    )
                );

            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            Configuration.Authorization.Providers.Add<NewWebAuthorizationProvider>();

            Configuration.Settings.Providers.Add<AppSettingProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
