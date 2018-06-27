using NewWeb.EntityFramework;
using EntityFramework.DynamicFilters;

namespace NewWeb.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly NewWebDbContext _context;

        public InitialHostDbBuilder(NewWebDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
