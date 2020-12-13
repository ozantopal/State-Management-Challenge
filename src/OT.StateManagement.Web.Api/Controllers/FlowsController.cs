using Microsoft.AspNetCore.Mvc;
using OT.StateManagement.Business.Service.Abstracts;
using OT.StateManagement.Web.Api.Contracts;
using System;

namespace OT.StateManagement.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowsController : ControllerBase
    {
        private readonly IFlowService _service;

        public FlowsController(IFlowService service)
        {
            _service = service;
        }

        // GET api/<Flows>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var flow = _service.Get(id);
            if (flow == null)
            {
                return NotFound();
            }

            return Ok(flow);
        }

        // POST api/<Flows>
        [HttpPost]
        public IActionResult Post([FromBody] FlowModel flow)
        {
            var addedFlow = _service.Add(new Business.Service.DTOs.Flow.FlowDto
            {
                Title = flow.Title
            });

            if (addedFlow == null)
            {
                return UnprocessableEntity();
            }

            return Created(new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/{addedFlow.Id}"), addedFlow);
        }

        // PUT api/<Flows>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] FlowModel flow)
        {
            bool succeeded = _service.Update(id, new Business.Service.DTOs.Flow.FlowDto
            {
                Title = flow.Title
            });

            return succeeded ? NoContent() : BadRequest();
        }

        // DELETE api/<Flows>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            bool succeeded = _service.Delete(id);

            return succeeded ? NoContent() : BadRequest();
        }
    }
}
