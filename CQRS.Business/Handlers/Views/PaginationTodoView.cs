using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Business.Handlers.Views {
    public class PaginationTodoView<T> {

        public int Total { get; private set; }
        public int Page { get; private set; }
        public int Limit { get; private set; }
        public List<T> Itens { get; private set; }

        public PaginationTodoView(List<T> itens , int total , int page , int limit) {
            Total = total;
            Page = page;
            Limit = limit;
            Itens = itens;
        }


    }
}
