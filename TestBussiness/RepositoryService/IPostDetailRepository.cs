using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.Entity;

namespace TestBussiness.RepositoryService
{
    public interface IPostDetailRepository : IRepository<PostDetail>
    {
        List<PostDetail> GetListByPostId(int postId);
    }
}
