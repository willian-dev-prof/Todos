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
    public class CreateTodoHandler : IRequestHandler<CreateTodoRequest, TodosResponse> {

        private readonly ControletodosContext _repository;
        private readonly IDbContextTransaction _transaction;

        public CreateTodoHandler(ControletodosContext context) {
            _repository = context;
            _transaction = context.Database.BeginTransaction();

        }
        public Task<TodosResponse> Handle(CreateTodoRequest request, CancellationToken cancellationToken) {

            if (!string.IsNullOrEmpty(request.Title) && !string.IsNullOrEmpty(request.Description)) {
                // Cria a entidade
                var customer = new Todo(request.Title,request.Complete,
                                        request.Complete ? DateTime.Now : null,request.Description);

                // Persiste a entidade no banco           
                try {
                    _repository.Add(customer);
                    _repository.SaveChanges();
                    _transaction.Commit();
                }
                catch (Exception e) {
                    _transaction.Rollback();
                    throw new TaskCanceledException(StringResources.transaction_error + e.Message);
                }

                // Retorna a resposta
                var result = new TodosResponse(){
                    Id = customer.Id,
                    Title = customer.Title,
                    Complete = customer.Complete,
                    DateComplete = customer.DateComplete,
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
