using System;
using System.Linq;
using System.Linq.Expressions;

namespace OT.StateManagement.DataAccess.EF.Repository.Abstracts
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Get();
        IQueryable<T> Get(params Expression<Func<T, object>>[] includes);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
