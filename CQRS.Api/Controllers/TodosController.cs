
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
using CQRS.Business.Handlers.Queries;
using CQRS.Business.Handlers.Definition;
using CQRS.Business.Handlers.QueryHandler;
using CQRS.Business.Handlers.Views;

namespace CQRS.Api.Controllers {

    [ApiController]
    [Route("todos")]
    public class TodosController : ControllerBase {

        private readonly IMediator _mediator;

        public TodosController(IMediator mediator ) {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult> Create(
                   [FromBody] CreateTodoLoad PayLoad) {

            return Ok(await _mediator.Send(new CreateTodoRequest(PayLoad.Title , PayLoad.Description , PayLoad.Complete)));
        }

        [HttpPut("Completed/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Updating(
                   [FromBody] UpdateTodoLoad PayLoad) {

            return Ok(await _mediator.Send(new UpdateTodoRequest(PayLoad.Id)));
        }

        [HttpPut("description/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdatingDesc(
                   [FromBody] UpdateDescTodoLoad PayLoad) {

            return Ok(await _mediator.Send(new UpdateDescTodoRequest(PayLoad.Id, PayLoad.Description)));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Deleting(
                    DeleteTodoLoad PayLoad) {

            await _mediator.Send(new DeleteTodoRequest(PayLoad.Id));
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaginationTodoView<TodosResponse>>> Get([FromQuery] int? page, int? limit) =>
             Ok(await _mediator.Send(new GetListTodoQuery(page.Value , limit.Value)));


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodosResponse>> GetByIdTodo(int? id) {

            return Ok(await _mediator.Send(new GetTodoByIdQuery(id.Value)));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("uncomplete")]
        public async Task<ActionResult<PaginationTodoView<TodosResponse>>> GetTodoUncomplete([FromQuery]  int? page , int? limit) {
            return Ok(await _mediator.Send(new GetUncompleteTodoQuery(page.Value, limit.Value)));
        }

    }
}
