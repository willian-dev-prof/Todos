using CQRS.Business.Commands.Requests;
using CQRS.Business.Commands.Responses;
using CQRS.Business.Exceptions;
using CQRS.Domain;
using CQRS.Domain.Entities;
using CQRS.Domain.Repository;
using CQRS.Infra.Context;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Business.Handlers.Concrete {
    public class TodoHandler : 
        IRequestHandler<CreateTodoRequest, TodosResponse>,
        IRequestHandler<DeleteTodoRequest>,
        IRequestHandler<UpdateTodoRequest, TodosResponse>,
        IRequestHandler<UpdateDescTodoRequest, TodosResponse> {

        private readonly ITodoRepository _repository;

        public TodoHandler(ITodoRepository repository) {
            this._repository = repository;
        }

        public async Task<TodosResponse> Handle(CreateTodoRequest request, CancellationToken cancellationToken) {

                // Cria a entidade
                var customer = new Todo(request.Title, request.Description , request.Complete);

                // Persiste a entidade no banco           
                await _repository.Add(customer);
                
                // Retorna a resposta
                var result = new TodosResponse(){
                    Id = customer.Id,
                    Title = customer.Title,
                    Complete = customer.Complete,
                    DateComplete = customer.DateComplete,
                    Description = customer.Description
                };

                return result;
            }

        public async Task<Unit> Handle(DeleteTodoRequest request, CancellationToken cancellationToken) {

            var todo = await _repository.GetId(request.Id);
            // Persiste a entidade no banco
             if(todo == null) 
                throw new BusinessException(StringResources.transaction_error);
            
            await _repository.Remove(request.Id);

            return Unit.Value;
        }

        public async Task<TodosResponse> Handle(UpdateTodoRequest request, CancellationToken cancellationToken) {
            var todo = await _repository.GetId(request.Id);
            // Persiste a entidade no banco
            if (todo == null) 
                throw new BusinessException(StringResources.transaction_error);
            
            
            todo =  todo.Completed();

            todo = await _repository.Update(todo);

            return new TodosResponse {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Complete = !todo.Complete,
                DateComplete = !todo.Complete ? todo.DateComplete : null
            };
        }

        public async Task<TodosResponse> Handle(UpdateDescTodoRequest request, CancellationToken cancellationToken) {
            var todo = await _repository.GetId(request.Id);

            if (todo == null)
                throw new BusinessException(StringResources.transaction_error);

            todo = todo.UpdateDescription(request.Description);

            await _repository.Update(todo);

            return new TodosResponse {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Complete = todo.Complete,
                DateComplete = todo.Complete ? todo.DateComplete : null
            };

        }

    }

}
