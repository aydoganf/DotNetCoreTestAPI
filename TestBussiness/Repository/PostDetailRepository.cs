using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.Connection;
using TestBussiness.Entity;
using TestBussiness.RepositoryService;
using System.Linq;

namespace TestBussiness.Repository
{
    public class PostDetailRepository : BaseRepository<PostDetail>, IPostDetailRepository
    {
        public PostDetailRepository(INHibernateHelper nHibernateHelper) : base(nHibernateHelper)
        {
        }

        public List<PostDetail> GetListByPostId(int postId)
        {
            using (var session = base.OpenSession())
            {
                return session.Query<PostDetail>()
                    .Where(o => o.Post.Id == postId)
                    .ToList();
            }
        }

        public override PostDetail Instance()
        {
            return new PostDetail();
        }
    }
}
