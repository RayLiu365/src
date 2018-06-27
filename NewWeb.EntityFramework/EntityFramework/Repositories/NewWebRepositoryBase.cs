using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace NewWeb.EntityFramework.Repositories
{
    public abstract class NewWebRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<NewWebDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected NewWebRepositoryBase(IDbContextProvider<NewWebDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class NewWebRepositoryBase<TEntity> : NewWebRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected NewWebRepositoryBase(IDbContextProvider<NewWebDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
