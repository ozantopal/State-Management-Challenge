using System;

namespace OT.StateManagement.Web.Api.Contracts
{
    public class TaskModel
    {
        public string Title { get; set; }
        public Guid StateId { get; set; }
    }
}
