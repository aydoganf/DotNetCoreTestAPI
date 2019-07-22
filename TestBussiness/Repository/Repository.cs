using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TestBussiness.Connection;
using TestBussiness.Context;
using TestBussiness.Entity;
using TestBussiness.RepositoryService;

namespace TestBussiness.Repository
{
    public abstract class BaseRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly ISession session;

        public BaseRepository(IContext context)
        {
            this.session = context.Session;
        }

        public virtual List<T> GetAll()
        {
            return session.Query<T>().ToList();
        }

        public virtual T GetById(int id)
        {
            return session.Get<T>(id, LockMode.Read);
        }

        public virtual void Delete(int id)
        {
            session.Delete(this.GetById(id));
        }

        public virtual T Update(T entity)
        {
            session.Update(entity);
            session.Flush();
            return entity;
        }

        public virtual T Insert(T entity)
        {
            int id = Convert.ToInt32(session.Save(entity));
            return GetById(id);
        }

        public T GetBy(Expression<Func<T, bool>> predicate)
        {
            return session.Query<T>().FirstOrDefault(predicate);
        }

        public List<T> GetListBy(Expression<Func<T, bool>> predicate)
        {
            return session.Query<T>().Where(predicate).ToList();
        }
    }
}
