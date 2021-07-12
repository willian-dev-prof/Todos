using CQRS.Domain.Entities;
using CQRS.Domain.Repository;
using CQRS.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Infra.Repositories {
    public class TodoRepository : ITodoRepository {
        private readonly ControletodosContext _context;
        private readonly IDbContextTransaction _transaction;

        public TodoRepository(ControletodosContext context) {
            _context = context;
            _transaction = context.Database.BeginTransaction();

        }

        public async Task<Todo> Add(Todo todo) {
            _context.Add(todo);
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
            return todo;

        }

        public async Task<Todo> GetId(int id) {
            return await _context.Todo.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Todo>> GetList(int limit, int page) {
            if(limit == 0 && page == 0)
                return await _context.Todo.OrderBy(a => a.Id).ToListAsync();
            return await _context.Todo.OrderBy(a => a.Id).Skip((page - 1) * limit).Take(limit).ToListAsync();
        }

        public async Task Remove(int id) {
            var todo = await _context.Todo.FirstOrDefaultAsync(a => a.Id == id);
            if (todo != null)
                 _context.Remove(todo);
                 await _context.SaveChangesAsync();
                 await _transaction.CommitAsync();
        }

        public async Task<Todo> Update(Todo todo) {
            _context.Todo.Update(todo);
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
            return todo;
        }
    }
}
