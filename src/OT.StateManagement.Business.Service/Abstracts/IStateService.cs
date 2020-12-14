using OT.StateManagement.Business.Service.DTOs.State;
using System;

namespace OT.StateManagement.Business.Service.Abstracts
{
    public interface IStateService
    {
        StateDto Get(Guid id);
        StateDto Add(StateDto entity);
        bool Update(Guid id, StateDto entity);
        bool Delete(Guid id);
    }
}
