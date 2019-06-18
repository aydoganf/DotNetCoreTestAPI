using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.Entity;

namespace TestBussiness.RepositoryService
{
    public interface IPostRepository : IRepository<Post>
    {        
        List<Post> GetByAuthorname(string authorName);
    }
}
