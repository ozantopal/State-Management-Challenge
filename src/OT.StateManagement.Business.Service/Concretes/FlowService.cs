using OT.StateManagement.Business.Service.Abstracts;
using OT.StateManagement.Business.Service.DTOs.Flow;
using OT.StateManagement.DataAccess.EF.Repository.Abstracts;
using OT.StateManagement.Domain.Entities;
using System;
using System.Linq;

namespace OT.StateManagement.Business.Service.Concretes
{
    public class FlowService : IFlowService
    {
        private readonly IRepository<Flow> _repository;
        public FlowService(IRepository<Flow> repository)
        {
            _repository = repository;
        }

        public FlowDto Get(Guid id)
        {
            return _repository.Get(x => x.Id == id)
                .Select(x => new FlowDto
                {
                    Id = x.Id,
                    Title = x.Title
                }).FirstOrDefault();
        }

        public FlowDto Add(FlowDto entity)
        {
            entity.Id = Guid.NewGuid();
            _repository.Add(new Flow
            {
                Id = entity.Id,
                Title = entity.Title
            });

            return entity;
        }

        public bool Update(Guid id, FlowDto entity)
        {
            var flow = _repository.Get(x => x.Id == id).FirstOrDefault();
            if (flow == null)
            {
                return false;
            }

            flow.Title = entity.Title;
            _repository.Update(flow);

            return true;
        }

        public bool Delete(Guid id)
        {
            var flow = _repository.Get(x => x.Id == id).FirstOrDefault();
            if (flow == null)
            {
                return false;
            }

            _repository.Delete(flow);

            return true;
        }
    }
}
