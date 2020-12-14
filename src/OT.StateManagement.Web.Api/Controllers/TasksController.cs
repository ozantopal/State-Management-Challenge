using Microsoft.AspNetCore.Mvc;
using OT.StateManagement.Business.Service.Abstracts;
using OT.StateManagement.Web.Api.Contracts;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OT.StateManagement.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;

        public TasksController(ITaskService service)
        {
            _service = service;
        }

        // GET api/<TasksController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var task = _service.Get(id);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        // POST api/<TasksController>
        [HttpPost]
        public IActionResult Post([FromBody] TaskModel task)
        {
            var addedTask = _service.Add(new Business.Service.DTOs.Task.TaskDto
            {
                Title = task.Title,
                StateId = task.StateId
            });

            if (addedTask == null)
            {
                return UnprocessableEntity();
            }

            return Created(new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/{addedTask.Id}"), addedTask);
        }

        [HttpPost("{id}/prev")]
        public IActionResult Previous(Guid id)
        {
            bool succeeded = _service.AsignNewState(id, Domain.Entities.TaskStateChangeType.Backward);

            return succeeded ? NoContent() : BadRequest();
        }

        [HttpPost("{id}/next")]
        public IActionResult Next(Guid id)
        {
            bool succeeded = _service.AsignNewState(id, Domain.Entities.TaskStateChangeType.Forward);

            return succeeded ? NoContent() : BadRequest();
        }

        // PUT api/<TasksController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] TaskModel task)
        {
            bool succeeded = _service.Update(id, new Business.Service.DTOs.Task.TaskDto
            {
                Title = task.Title
            });

            return succeeded ? NoContent() : BadRequest();
        }

        // DELETE api/<TasksController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            bool succeeded = _service.Delete(id);

            return succeeded ? NoContent() : BadRequest();
        }
    }
}
