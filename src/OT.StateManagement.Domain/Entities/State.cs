using System;
using System.Collections.Generic;

namespace OT.StateManagement.Domain.Entities
{
    public class State : BaseEntity
    {
        public string Title { get; set; }
        public Guid FlowId { get; set; }
        public Guid? PreviousStateId { get; set; }
        public Guid? NextStateId { get; set; }

        public virtual Flow Flow { get; set; }
        public virtual State PreviousState { get; set; }
        public virtual State NextState { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}