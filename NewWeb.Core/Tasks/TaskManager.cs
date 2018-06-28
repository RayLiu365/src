using Abp.Authorization;
using Abp.Domain.Services;
using Abp.Events.Bus;
using Abp.Runtime.Session;
using NewWeb.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewWeb.Tasks
{
    public class TaskManager : DomainService, ITaskManager
    {
        private readonly IPermissionChecker _permissionChecker;
        private readonly IAbpSession _abpSession;
        private readonly IEventBus _eventBus;

        public TaskManager(IPermissionChecker permissionChecker, IAbpSession abpSession, IEventBus eventBus)
        {
            _permissionChecker = permissionChecker;
            _abpSession = abpSession;
            _eventBus = eventBus;
        }

        public void AssignTaskToPerson(Task task, User user)
        {
            //已经分配，就不再分配
            if (task.AssignedPersonId.HasValue && task.AssignedPersonId.Value == user.Id)
            {
                return;
            }

            if (task.State != TaskState.Open)
            {
                throw new ApplicationException("处于非活动状态的任务不能分配！");
            }

            task.AssignedPersonId = user.Id;


            //使用领域事件触发发送通知操作
            _eventBus.Trigger(new TaskAssignedEventData(task, user));
        }
    }
}
