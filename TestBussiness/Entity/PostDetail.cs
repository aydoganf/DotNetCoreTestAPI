using System;
using System.Collections.Generic;
using System.Text;

namespace TestBussiness.Entity
{
    public class PostDetail : IEntity
    {
        public virtual int Id { get; protected set; }
        public virtual string PostContent { get; set; }
        public virtual DateTime ModifyDate { get; set; }
        public virtual Post Post { get; set; }

        public static PostDetail BuildPostDetail(int postDetailId, string postContent, DateTime modifyDate)
        {
            return new PostDetail()
            {
                Id = postDetailId,
                PostContent = postContent,
                ModifyDate = modifyDate
            };
        }
    }
}
