using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBussiness.Entity;

namespace TestWebAPI.Models
{
    public class PostDetailEntity
    {
        public int PostDetailId { get; set; }
        public string PostContent { get; set; }
        public DateTime ModifyDate { get; set; }

    }
}
