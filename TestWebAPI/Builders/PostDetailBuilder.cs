using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBussiness.Entity;
using TestWebAPI.Models;

namespace TestWebAPI.Builders
{
    public class PostDetailBuilder
    {
        public static PostDetail Build(PostDetailEntity obj)
        {
            return new PostDetail()
            {
                ModifyDate = obj.ModifyDate,
                PostContent = obj.PostContent
            };
        }
    }
}
