using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Business.Commands.Responses {
    public class TodosResponse {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Complete { get; set; }
        public DateTime? DateComplete { get; set; }
        public string Description { get; set; }
    }
}
