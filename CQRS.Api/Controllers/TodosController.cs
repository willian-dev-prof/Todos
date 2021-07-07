
using CQRS.Api.Properties;
using CQRS.Domain;
using CQRS.Business.Commands.Requests;
using CQRS.Business.Commands.Responses;
using CQRS.Domain.Entities;
using CQRS.Infra.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace CQRS.Api.Controllers {

    [ApiController]
    [Route("todos")]
    public class TodosController : ControllerBase {

        private readonly IMediator _mediator;
        private readonly ControletodosContext _repository;

        public TodosController(IMediator mediator , ControletodosContext repository) {
            _mediator = mediator;
            _repository = repository;
        }

        [HttpPost]
        [Route("todos")]
        public async Task<ActionResult> Create(
                   [FromBody] CreateTodoRequest command) {

            return Ok(await _mediator.Send(command));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("todos")]
        public async Task<ActionResult> Updating(
                   [FromBody] UpdateTodoRequest command) {

            Todo customer = _repository.Todo.FirstOrDefault(a => a.Id == command.Id);

            return customer == null ? NotFound(StringResources.id_not_todo_in_database) 
                                    : Ok(await _mediator.Send(command));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("todos/description")]
        public async Task<ActionResult> UpdatingDesc(
                   [FromBody] UpdateDescTodoRequest command) {

            Todo customer = _repository.Todo.FirstOrDefault(a => a.Id == command.Id);

            return customer == null ? NotFound(StringResources.id_not_todo_in_database) 
                                    : Ok(await _mediator.Send(command));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("todos")]
        public async Task<ActionResult> Deleting(
                   [FromBody] DeleteTodoRequest command) {

            Todo customer = _repository.Todo.FirstOrDefault(a => a.Id == command.Id);

            return customer == null ? NotFound(StringResources.id_not_todo_in_database) 
                                    : Ok(await _mediator.Send(command));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("todos")]
        public async Task<ActionResult<List<Todo>>> GetAllTodo() {

            var response = await _repository.Todo.OrderBy(todo => todo.Id).ToListAsync();

            return response.Count == 0 ? NotFound(StringResources.no_todo_was_found_in_base)
                                       : Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("todos/limit")]
        public async Task<ActionResult<List<Todo>>> GetAllTodoLimit([Range(1, int.MaxValue)] int limite = 1 ,
            [Range(1,int.MaxValue)]int page = 1) {

            var response = await _repository.Todo.OrderBy(todo => todo.Id)
                .Skip((page-1)*limite).Take(limite).ToListAsync();

            return response.Count == 0 ? NotFound(StringResources.no_todo_was_found_in_base)
                                       : Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("todos/id")]
        public async Task<ActionResult<Todo>> GetByIdTodo([Range(1, int.MaxValue)] int id) {

            Todo response = await _repository.Todo.FirstOrDefaultAsync(a => a.Id == id);

            return response == null ? NotFound(StringResources.id_not_todo_in_database) 
                                    : Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("todos/uncomplete")]
        public async Task<ActionResult<List<Todo>>> GetTodoUncomplete() {
            var response = await _repository.Todo.Where(a => a.Complete == false).ToListAsync();

            return response.Count == 0 ? NotFound(StringResources.msg_error_uncomplete) 
                                       : Ok(response);
        }

    }
}
