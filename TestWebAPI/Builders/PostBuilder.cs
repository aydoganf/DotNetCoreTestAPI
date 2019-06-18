using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBussiness.Entity;
using TestWebAPI.Models;

namespace TestWebAPI.Builders
{
    public class PostBuilder
    {
        public static Post Build(PostEntity obj)
        {
            return new Post()
            {
                PostAuthor = obj.PostAuthor,
                PostDate = obj.PostDate
            };
        }
    }
}
