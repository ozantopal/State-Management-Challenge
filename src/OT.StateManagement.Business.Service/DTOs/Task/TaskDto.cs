using System;

namespace OT.StateManagement.Business.Service.DTOs.Task
{
    public class TaskDto : BaseDto
    {
        public string Title { get; set; }
        public Guid StateId { get; set; }
    }
}
