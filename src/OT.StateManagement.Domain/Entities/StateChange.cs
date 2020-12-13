using System;

namespace OT.StateManagement.Domain.Entities
{
    public class StateChange : BaseEntity
    {
        public Guid TaskId { get; set; }
        public TaskStateChangeType TypeOfChange { get; set; }

        public Task Task { get; set; }
    }
}
