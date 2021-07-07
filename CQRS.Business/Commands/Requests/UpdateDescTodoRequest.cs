using CQRS.Business.Commands.Responses;
using CQRS.Domain;
using CQRS.Domain.Models.ValidationAtributes;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Business.Commands.Requests {
    public class UpdateDescTodoRequest : IRequest<TodosResponse> {
        [Range(1, int.MaxValue, ErrorMessageResourceName = nameof(StringResources.id_not_todo_in_database), ErrorMessageResourceType = typeof(StringResources))]
        public int Id { get; set; }
        [Required(ErrorMessageResourceName = nameof(StringResources.this_field_is_required), ErrorMessageResourceType = typeof(StringResources))]
        [StringLength(50, ErrorMessageResourceName = nameof(StringResources.very_long_descripition), ErrorMessageResourceType = typeof(StringResources))]
        [ValidationShort(ErrorMessageResourceName = nameof(StringResources.very_short_description), ErrorMessageResourceType = typeof(StringResources))]
        public string Description { get; set; }
    }
}
