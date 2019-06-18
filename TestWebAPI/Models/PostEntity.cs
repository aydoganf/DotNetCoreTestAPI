using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBussiness.Entity;

namespace TestWebAPI.Models
{
    public class PostEntity
    {
        public int PostId { get; set; }
        public string PostAuthor { get; set; }
        public DateTime PostDate { get; set; }

    }
}
