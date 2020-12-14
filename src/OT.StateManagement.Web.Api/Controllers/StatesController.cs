using Microsoft.AspNetCore.Mvc;
using OT.StateManagement.Business.Service.Abstracts;
using OT.StateManagement.Web.Api.Contracts;
using System;

namespace OT.StateManagement.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly IStateService _service;

        public StatesController(IStateService service)
        {
            _service = service;
        }

        // GET api/<StatesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var state = _service.Get(id);
            if (state == null)
            {
                return NotFound();
            }

            return Ok(state);
        }

        // POST api/<StatesController>
        [HttpPost]
        public IActionResult Post([FromBody] StateModel state)
        {
            var addedState = _service.Add(new Business.Service.DTOs.State.StateDto
            {
                Title = state.Title,
                FlowId = state.FlowId,
                NextStateId = state.NextStateId,
                PreviousStateId = state.PreviousStateId
            });

            if (addedState == null)
            {
                return UnprocessableEntity();
            }

            return Created(new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/{addedState.Id}"), addedState);
        }

        // PUT api/<StatesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] StateModel state)
        {
            bool succeeded = _service.Update(id, new Business.Service.DTOs.State.StateDto
            {
                Title = state.Title,
                FlowId = state.FlowId,
                PreviousStateId = state.PreviousStateId,
                NextStateId = state.NextStateId
            });

            return succeeded ? NoContent() : BadRequest();
        }

        // DELETE api/<StatesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            bool succeeded = _service.Delete(id);

            return succeeded ? NoContent() : BadRequest();
        }
    }
}
