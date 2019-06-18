using System;
using System.Collections.Generic;
using System.Text;

namespace TestBussiness.Entity
{
    public class Post : IEntity
    {
        public virtual int Id { get; protected set; }
        public virtual string PostAuthor { get; set; }
        public virtual DateTime PostDate { get; set; }
        public virtual ICollection<PostDetail> PostDetails { get; set; }

        public static Post Build(int postId, string postAuthor, DateTime postDate)
        {
            return new Post()
            {
                Id = postId,
                PostAuthor = postAuthor,
                PostDate = postDate
            };
        }
    }
}
