using OT.StateManagement.Business.Service.Abstracts;
using OT.StateManagement.Business.Service.DTOs.Task;
using OT.StateManagement.DataAccess.EF.Repository.Abstracts;
using OT.StateManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OT.StateManagement.Business.Service.Concretes
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<Task> _repository;
        private readonly IRepository<StateChange> _scRepository;
        public TaskService(IRepository<Task> repository,
                           IRepository<StateChange> scRepository)
        {
            _repository = repository;
            _scRepository = scRepository;
        }

        public TaskDto Get(Guid id)
        {
            return _repository.Get(x => x.Id == id)
                .Select(x => new TaskDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    StateId = x.StateId
                }).FirstOrDefault();
        }

        public TaskDto Add(TaskDto entity)
        {
            entity.Id = Guid.NewGuid();
            _repository.Add(new Task
            {
                Id = entity.Id,
                Title = entity.Title,
                StateId = entity.StateId,
                StateChanges = new List<StateChange>
                {
                    new StateChange{Id = Guid.NewGuid(), TaskId = entity.Id, TypeOfChange = TaskStateChangeType.Forward}
                }
            });

            return entity;
        }

        public bool Update(Guid id, TaskDto entity)
        {
            var task = _repository.Get(x => x.Id == id).FirstOrDefault();
            if (task == null)
            {
                return false;
            }

            task.Title = entity.Title;
            _repository.Update(task);

            return true;
        }

        public bool AsignNewState(Guid id, TaskStateChangeType changeType)
        {
            var task = _repository.Get(x => x.Id == id, x => x.State, x => x.StateChanges).FirstOrDefault();
            if (task == null)
            {
                return false;
            }

            switch (changeType)
            {
                case TaskStateChangeType.Backward:
                    if (!task.State.PreviousStateId.HasValue)
                    {
                        return false;
                    }

                    task.StateId = task.State.PreviousStateId.Value;
                    _repository.Update(task);

                    _scRepository.Add(new StateChange
                    {
                        Id = Guid.NewGuid(),
                        TaskId = task.Id,
                        TypeOfChange = TaskStateChangeType.Backward
                    });

                    return true;
                case TaskStateChangeType.Forward:
                    if (!task.State.NextStateId.HasValue)
                    {
                        return false;
                    }

                    task.StateId = task.State.NextStateId.Value;
                    _repository.Update(task);

                    _scRepository.Add(new StateChange
                    {
                        Id = Guid.NewGuid(),
                        TaskId = task.Id,
                        TypeOfChange = TaskStateChangeType.Forward
                    });

                    return true;
                default:
                    return false;
            }
        }

        public bool Delete(Guid id)
        {
            var task = _repository.Get(x => x.Id == id).FirstOrDefault();
            if (task == null)
            {
                return false;
            }

            _repository.Delete(task);

            return true;
        }
    }
}
