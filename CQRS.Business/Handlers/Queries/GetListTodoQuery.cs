using CQRS.Business.Commands.Responses;
using CQRS.Business.Handlers.Definition;
using CQRS.Business.Handlers.Views;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Business.Handlers.QueryHandler {
    public class GetListTodoQuery : GetPageLimiteBase , IRequest<PaginationTodoView<TodosResponse>>{

        public GetListTodoQuery(int page , int limit) : base(page, limit) {

        }

    }
}
