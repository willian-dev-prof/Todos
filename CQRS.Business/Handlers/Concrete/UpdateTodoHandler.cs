using CQRS.Business.Commands.Requests;
using CQRS.Business.Commands.Responses;
using CQRS.Domain;
using CQRS.Domain.Entities;
using CQRS.Infra.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Business.Handlers.Concrete {
    public class UpdateTodoHandler : IRequestHandler<UpdateTodoRequest, TodosResponse> {

        private readonly ControletodosContext _repository;
        private readonly IDbContextTransaction _transaction;

        public UpdateTodoHandler(ControletodosContext context) {
            _repository = context;
            _transaction = context.Database.BeginTransaction();
        }
        public async Task<TodosResponse> Handle(UpdateTodoRequest request, CancellationToken cancellationToken) {

            if (request.Id != 0) {
                Todo customer = _repository.Todo.FirstOrDefault(a => a.Id == request.Id);
                // atualiza entidade
                customer.DateComplete = request.Complete ? DateTime.Now : null;
                customer.Complete = request.Complete;
                // Persiste a entidade no banco           
                try {
                    _repository.Update(customer);
                    await _repository.SaveChangesAsync();
                    _transaction.Commit();
                }
                catch (Exception e) {
                    _transaction.Rollback();
                    throw new Exception(StringResources.transaction_error + e.Message);
                }
                // Retorna a resposta
                var result = new TodosResponse {
                    Id = customer.Id,Title = customer.Title,
                    Complete = customer.Complete,DateComplete = customer.DateComplete,
                    Description = customer.Description
                };

                return result;
            }
            else {
                throw new ArgumentException(StringResources.fields_are_required);
            }
        }
    }
}
