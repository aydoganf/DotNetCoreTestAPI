using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Text;
using TestBussiness.Entity;

namespace TestBussiness.Mappers
{
    public class PostDetailMap : ClassMap<PostDetail>
    {
        public PostDetailMap()
        {
            Table("PostDetail");
            LazyLoad();
            Id(x => x.Id).GeneratedBy.Identity().Column("PostDetailId");
            Map(x => x.ModifyDate).Column("ModifyDate");
            Map(x => x.PostContent).Column("PostContent");
            References(x => x.Post, "PostId").ForeignKey("none");
        }
    }
}
