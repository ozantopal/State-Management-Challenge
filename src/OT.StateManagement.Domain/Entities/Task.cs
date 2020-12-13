using System;
using System.Collections.Generic;

namespace OT.StateManagement.Domain.Entities
{
    public class Task : BaseEntity
    {
        public string Title { get; set; }
        public Guid StateId { get; set; }

        public virtual State State { get; set; }
        public ICollection<StateChange> StateChanges { get; set; }
    }
}
