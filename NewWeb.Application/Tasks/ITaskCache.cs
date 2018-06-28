using Abp.Domain.Entities.Caching;
using NewWeb.Tasks.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewWeb.Tasks
{
    public interface ITaskCache : IEntityCache<TaskCacheItem>
    {
    }
}
