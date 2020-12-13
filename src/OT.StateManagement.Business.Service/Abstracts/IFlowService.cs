using OT.StateManagement.Business.Service.DTOs.Flow;
using System;

namespace OT.StateManagement.Business.Service.Abstracts
{
    public interface IFlowService
    {
        FlowDto Get(Guid id);
        FlowDto Add(FlowDto entity);
        bool Update(Guid id, FlowDto entity);
        bool Delete(Guid id);
    }
}
