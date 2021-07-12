using CQRS.Business.Commands.Responses;
using CQRS.Business.Handlers.Definition;
using CQRS.Business.Handlers.Views;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Business.Handlers.Queries {
    public class GetUncompleteTodoQuery : GetPageLimiteBase , IRequest<PaginationTodoView<TodosResponse>> {

        public GetUncompleteTodoQuery(int page , int limit) : base(page, limit){

        }
    }
}
