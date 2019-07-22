using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.Entity;

namespace TestBussiness.RepositoryService
{
    public interface IRepository<T> where T : IEntity
    {
        List<T> GetAll();
        T GetById(int id);
        T Update(T entity);
        T Insert(T entity);
        void Delete(int id);
    }
}
