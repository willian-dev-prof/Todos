using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQRS.Business.Commands.Responses;
using CQRS.Business.Exceptions;
using CQRS.Business.Handlers.Queries;
using CQRS.Business.Handlers.Views;
using CQRS.Domain;
using CQRS.Domain.Entities;
using CQRS.Infra.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Business.Handlers.QueryHandler {
    public class TodoQueryHandlers :
        IRequestHandler<GetListTodoQuery, PaginationTodoView<TodosResponse>>,
        IRequestHandler<GetTodoByIdQuery, TodosResponse>,
        IRequestHandler<GetUncompleteTodoQuery, PaginationTodoView<TodosResponse>> {

        private readonly ControletodosContext _context;

        public TodoQueryHandlers(ControletodosContext context) {
            this._context = context;
        }
        public async Task<TodosResponse> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken) {

            var todo = await _context.Todo.FirstOrDefaultAsync(a => a.Id == request.Id);

            if (todo != null) {
                return new TodosResponse {
                    Id = todo.Id,
                    Title = todo.Title,
                    Description = todo.Description,
                    Complete = todo.Complete,
                    DateComplete = todo.Complete ? todo.DateComplete : null
                };
            }
            else {
                throw new BusinessException(StringResources.id_not_todo_in_database);
            }
        }

        public async Task<PaginationTodoView<TodosResponse>> Handle(GetUncompleteTodoQuery request, CancellationToken cancellationToken) {
            
            var todos = await _context.Todo.Where(a => a.Complete == false).OrderBy(a => a.Id)
                             .Skip(request.Indexof).Take(request.Limit).ToListAsync();

            if (todos != null) {
                var total = todos.Count;

                var itens = todos.Select(a => new TodosResponse {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description
                }).ToList();

                return new PaginationTodoView<TodosResponse>(itens, total, request.Page, request.Limit);
            }
            else {
                throw new BusinessException(StringResources.no_todo_was_found_in_base);
            }
        }

        public async Task<PaginationTodoView<TodosResponse>> Handle(GetListTodoQuery request, CancellationToken cancellationToken) {

            var todos = await _context.Todo.OrderBy(a => a.Id)
                             .Skip(request.Indexof).Take(request.Limit).ToListAsync();
            if (todos != null) {
                var total = todos.Count;

                var itens = todos.Select(a => new TodosResponse {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    Complete = a.Complete,
                    DateComplete = a.Complete ? a.DateComplete : null
                }).ToList();

                return new PaginationTodoView<TodosResponse>(itens, total, request.Page, request.Limit);
            }
            else {
                throw new BusinessException(StringResources.no_todo_was_found_in_base);
            }
        }
    }
}
