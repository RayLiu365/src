﻿using Abp.Domain.Services;
using NewWeb.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewWeb.Tasks
{
    public interface ITaskManager : IDomainService
    {
        void AssignTaskToPerson(Task task, User user);
    }
}
