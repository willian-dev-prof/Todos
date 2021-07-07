using CQRS.Business.Commands.Responses;
using CQRS.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Business.Commands.Requests {
    
    public class DeleteTodoRequest : IRequest<TodosResponse> {
        [Range(1, int.MaxValue, ErrorMessageResourceName = nameof(StringResources.id_not_todo_in_database), ErrorMessageResourceType = typeof(StringResources))]
        public int Id { get; set; }
    }
}
