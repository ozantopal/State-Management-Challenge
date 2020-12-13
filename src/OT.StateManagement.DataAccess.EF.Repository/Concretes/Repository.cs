using Microsoft.EntityFrameworkCore;
using OT.StateManagement.DataAccess.EF.Repository.Abstracts;
using OT.StateManagement.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace OT.StateManagement.DataAccess.EF.Repository.Concretes
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        public StateContext Context { get; }
        public Repository(StateContext context)
        {
            Context = context;
        }

        public IQueryable<T> Get()
        {
            return Context.Set<T>()
                .AsQueryable();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>()
                .Where(predicate)
                .AsQueryable();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = Context.Set<T>()
                .AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.Where(predicate);
        }

        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
            Context.SaveChanges();
        }

        public void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
            Context.SaveChanges();
        }
    }
}
