using System;

namespace OT.StateManagement.Business.Service.DTOs.State
{
    public class StateDto : BaseDto
    {
        public string Title { get; set; }
        public Guid FlowId { get; set; }
        public Guid? PreviousStateId { get; set; }
        public Guid? NextStateId { get; set; }
    }
}
