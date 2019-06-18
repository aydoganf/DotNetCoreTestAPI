using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.Entity;

namespace TestBussiness.RepositoryService
{
    public interface IRepository<T> where T : IEntity
    {
        T Instance();
        List<T> GetAll();
        T GetById(int id);
        T Update(T entity, int id);
        T Insert(T entity);
        void Delete(int id);
    }
}
