using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.Entity;

namespace TestBussiness.Mappers
{
    public class PostMap : ClassMap<Post>
    {
        public PostMap()
        {
            Table("Post");
            LazyLoad();
            Id(x => x.Id).GeneratedBy.Identity().Column("PostId");
            Map(x => x.PostAuthor).Column("PostAuthor");
            Map(x => x.PostDate).Column("PostDate");

            HasMany(x => x.PostDetails)
                .KeyColumns.Add("PostId");
        }
    }
}
