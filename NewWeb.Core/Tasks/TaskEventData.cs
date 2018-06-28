using Abp.Events.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewWeb.Tasks
{
    public class TaskEventData : EventData
    {
        public Task Task { get; set; }
    }
}
