using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using NewWeb.EntityFramework;

namespace NewWeb.Migrator
{
    [DependsOn(typeof(NewWebDataModule))]
    public class NewWebMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<NewWebDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}