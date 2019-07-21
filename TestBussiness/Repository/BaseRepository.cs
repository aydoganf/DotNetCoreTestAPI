using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestBussiness.Connection;
using TestBussiness.Entity;
using TestBussiness.RepositoryService;

namespace TestBussiness.Repository
{
    public abstract class BaseRepository<T> : IRepository<T> where T : IEntity
    {
        protected readonly INHibernateHelper nhibernateHelper;

        private ISession _session;
        protected ISession session
        {
            get
            {
                if (_session == null)
                {
                    _session = this.OpenSession();
                }
                return _session;
            }
        }

        public BaseRepository(INHibernateHelper nHibernateHelper)
        {
            this.nhibernateHelper = nHibernateHelper;
        }

        public virtual ISession OpenSession()
        {
            return this.nhibernateHelper.OpenSession();
        }

        public virtual List<T> GetAll()
        {
            return session.Query<T>().ToList();
        }

        public virtual T GetById(int id)
        {
            return session.Load<T>(id, NHibernate.LockMode.Read);
        }

        public virtual void Delete(int id)
        {
            session.Delete(this.GetById(id));
        }

        [Obsolete]
        public virtual T Update(T entity, int id)
        {
            // todo: think about here again
            T o = session.Load<T>(id);
            if (o != null)
            {
                o = entity;
                session.Update(entity);
                session.Flush();
                return o;
            }
            return (default);
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

        public abstract T Instance();
    }
}
