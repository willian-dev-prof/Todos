using CQRS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Repository {
    public interface ITodoRepository {
        Task<Todo> Add(Todo todo);
        Task<Todo> Update(Todo todo);
        Task Remove(int id);
        Task<Todo> GetId(int id);
        Task<List<Todo>> GetList(int limit , int page);

    }
}
