﻿using System;

namespace OT.StateManagement.Web.Api.Contracts
{
    public class StateModel
    {
        public string Title { get; set; }
        public Guid FlowId { get; set; }
        public Guid? PreviousStateId { get; set; }
        public Guid? NextStateId { get; set; }
    }
}
