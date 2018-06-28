

using NewWeb.Dto;

namespace NewWeb.Tasks.Dtos
{
    public class GetTasksInput : PagedSortedAndFilteredInputDto
    {
        public TaskState? State { get; set; }

        public int? AssignedPersonId { get; set; }
    }
}
