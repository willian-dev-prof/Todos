using CQRS.Business.Commands.Requests;
using CQRS.Business.Commands.Responses;
using CQRS.Domain;
using CQRS.Domain.Entities;
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
    public class DeleteTodoHandler : IRequestHandler<DeleteTodoRequest, TodosResponse> {

        private readonly ControletodosContext _repository;
        private readonly IDbContextTransaction _transaction;
        public DeleteTodoHandler(ControletodosContext context) {
            _repository = context;
            _transaction = context.Database.BeginTransaction();

        }
        public Task<TodosResponse> Handle(DeleteTodoRequest request, CancellationToken cancellationToken) {

            if (request.Id != 0) {
                Todo customer = _repository.Todo.FirstOrDefault(a => a.Id == request.Id);
                // Persiste a entidade no banco           
                try {
                    _repository.Remove(customer);
                    _repository.SaveChanges();
                    _transaction.Commit();
                }
                catch (Exception e) {
                    _transaction.Rollback();
                    throw new TaskCanceledException(StringResources.transaction_error + e.Message);
                }
                // Retorna a resposta
                var result = new TodosResponse {
                    Id = customer.Id,Title = customer.Title,
                    Complete = customer.Complete,DateComplete = customer.DateComplete,
                    Description = customer.Description
                };

                return Task.FromResult(result);
            }
            else {
                throw new ArgumentException(StringResources.fields_are_required);
            }
        }
    }
}
