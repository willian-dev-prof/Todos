using CQRS.Business.Commands.Responses;
using CQRS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Business.Handlers.Queries {
    public class GetTodoByIdQuery : IRequest<TodosResponse> {

        public int Id { get; private set; }
        public GetTodoByIdQuery(int id) {
            Id = id;
        }
    }
}
