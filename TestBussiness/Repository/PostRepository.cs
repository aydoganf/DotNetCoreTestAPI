using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestBussiness.Connection;
using TestBussiness.Entity;
using TestBussiness.RepositoryService;

namespace TestBussiness.Repository
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(INHibernateHelper nHibernateHelper) : base(nHibernateHelper)
        {
        }

        public List<Post> GetByAuthorname(string authorName)
        {
            using (var session = base.OpenSession())
            {
                return session.Query<Post>()
                    .Where(p => p.PostAuthor == authorName)
                    .ToList();
            }
        }

        public override Post Instance()
        {
            throw new NotImplementedException();
        }
    }
}
