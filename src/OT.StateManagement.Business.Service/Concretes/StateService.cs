using OT.StateManagement.Business.Service.Abstracts;
using OT.StateManagement.Business.Service.DTOs.State;
using OT.StateManagement.DataAccess.EF.Repository.Abstracts;
using OT.StateManagement.Domain.Entities;
using System;
using System.Linq;

namespace OT.StateManagement.Business.Service.Concretes
{
    public class StateService : IStateService
    {
        private readonly IRepository<State> _repository;
        public StateService(IRepository<State> repository)
        {
            _repository = repository;
        }

        public StateDto Get(Guid id)
        {
            return _repository.Get()
                .Where(x => x.Id == id)
                .Select(x => new StateDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    FlowId = x.FlowId,
                    PreviousStateId = x.PreviousStateId,
                    NextStateId = x.NextStateId
                }).FirstOrDefault();
        }

        public StateDto Add(StateDto entity)
        {
            entity.Id = Guid.NewGuid();
            _repository.Add(new State
            {
                Id = entity.Id,
                Title = entity.Title,
                FlowId = entity.FlowId,
                PreviousStateId = entity.PreviousStateId,
                NextStateId = entity.NextStateId
            });
            UpdateRelationalStatesWithNewState(entity);

            return entity;
        }

        public bool Update(Guid id, StateDto entity)
        {
            var state = _repository.Get().FirstOrDefault(x => x.Id == id);
            if (state == null)
            {
                return false;
            }

            state.Title = entity.Title;
            state.FlowId = entity.FlowId;
            state.PreviousStateId = entity.PreviousStateId;
            state.NextStateId = entity.NextStateId;

            _repository.Update(state);
            UpdateRelationalStatesWithNewState(entity);

            return true;
        }

        public bool Delete(Guid id)
        {
            var state = _repository.Get().FirstOrDefault(x => x.Id == id);
            if (state == null)
            {
                return false;
            }

            UpdateRelationalStatesWithExState(state);
            _repository.Delete(state);

            return true;
        }

        private void UpdateRelationalStatesWithNewState(StateDto state)
        {
            if (state.PreviousStateId.HasValue)
            {
                var prevState = _repository.Get(x => x.Id == state.PreviousStateId.Value).FirstOrDefault();
                prevState.NextStateId = state.Id;
                _repository.Update(prevState);
            }
            else
            {
                var formerPrevState = _repository.Get(x => x.NextStateId == state.Id).FirstOrDefault();
                if (formerPrevState == null)
                {
                    return;
                }
                formerPrevState.NextStateId = null;
                _repository.Update(formerPrevState);
            }

            if (state.NextStateId.HasValue)
            {
                var nextState = _repository.Get(x => x.Id == state.NextStateId.Value).FirstOrDefault();
                nextState.PreviousStateId = state.Id;
                _repository.Update(nextState);
            }
            else
            {
                var formerNextState = _repository.Get(x => x.PreviousStateId == state.Id).FirstOrDefault();
                if (formerNextState == null)
                {
                    return;
                }
                formerNextState.PreviousStateId = null;
                _repository.Update(formerNextState);
            }
        }

        private void UpdateRelationalStatesWithExState(State state)
        {
            if (state.PreviousStateId.HasValue)
            {
                var prevState = _repository.Get().FirstOrDefault(x => x.Id == state.PreviousStateId.Value);
                if (prevState != null)
                {
                    prevState.NextStateId = state.NextStateId;
                    _repository.Update(prevState);
                }
            }

            if (state.NextStateId.HasValue)
            {
                var nextState = _repository.Get().FirstOrDefault(x => x.Id == state.NextStateId.Value);
                if (nextState != null)
                {
                    nextState.PreviousStateId = state.PreviousStateId;
                    _repository.Update(nextState);
                }
            }
        }
    }
}
