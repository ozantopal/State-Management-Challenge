using System.Collections.Generic;

namespace OT.StateManagement.Domain.Entities
{
    public class Flow : BaseEntity
    {
        public string Title { get; set; }

        public ICollection<State> States { get; set; }
    }
}
