using OT.StateManagement.Business.Service.DTOs.Task;
using OT.StateManagement.Domain.Entities;
using System;

namespace OT.StateManagement.Business.Service.Abstracts
{
    public interface ITaskService
    {
        TaskDto Get(Guid id);
        TaskDto Add(TaskDto entity);
        bool Update(Guid id, TaskDto entity);
        bool AsignNewState(Guid id, TaskStateChangeType changeType);
        bool Delete(Guid id);
    }
}
