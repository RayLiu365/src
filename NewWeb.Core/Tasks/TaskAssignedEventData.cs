using NewWeb.Authorization.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewWeb.Tasks
{
    public class TaskAssignedEventData : TaskEventData
    {
        public User User { get; set; }
        public TaskAssignedEventData(Task task, User user)
        {
            this.Task = task;
            this.User = user;
        }
    }
}
