using Abp.Authorization.Users;
using Abp.Dependency;
using NewWeb.Authorization.Users;
using NewWeb.MultiTenancy;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewWeb
{
    public class MyExternalAuthSource : DefaultExternalAuthenticationSource<Tenant, User>, ITransientDependency
    {
        public override string Name
        {
            get { return "MyLdapAuthenticationSource"; }
        }

        public override Task<bool> TryAuthenticateAsync(string userNameOrEmailAddress, string plainPassword, Tenant tenant)
        {
            //TODO: authenticate user and return true or false
            return CheckAuthenticate(userNameOrEmailAddress, plainPassword);
        }

        private Task<bool> CheckAuthenticate(string userName, string userPwd)
        {            
            string strLdapPath = System.Configuration.ConfigurationManager.AppSettings["LdapPath"];
            bool checkResult = false;
            DirectoryEntry entry = new DirectoryEntry(strLdapPath, userName, userPwd)
            {
                //AuthenticationType = AuthenticationTypes.None
            };

            try
            {
                object obj = entry.NativeObject;
                checkResult = true;
            }
            catch(Exception ex)
            {
                throw new Exception("Error authenticating user." + ex.Message);
            }

            return TaskEx.FromResult(checkResult);
        }
    }
}
