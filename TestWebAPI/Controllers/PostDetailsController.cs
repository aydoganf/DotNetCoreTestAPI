using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestBussiness.Connection;
using TestBussiness.Entity;
using TestBussiness.RepositoryService;
using TestWebAPI.Builders;
using TestWebAPI.Models;

namespace TestWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostDetailsController : ControllerBase
    {
        private INHibernateHelper NHibernateHelper;
        private IPostDetailRepository PostDetailRepository;

        public PostDetailsController(INHibernateHelper nHibernateHelper, IPostDetailRepository postDetailRepository)
        {
            this.NHibernateHelper = nHibernateHelper;
            this.PostDetailRepository = postDetailRepository;
        }

        // GET: api/PostDetails
        [HttpGet]
        public List<PostDetail> Get()
        {
            return this.PostDetailRepository.GetAll();
        }

        // GET: api/PostDetails/5
        [HttpGet("{id}")]
        public PostDetail Get(int id)
        {
            return this.PostDetailRepository.GetById(id);
        }

        [HttpGet("post/{postId}")]
        public List<PostDetail> GetPostDetailsByPostId(int postId)
        {
            return this.PostDetailRepository.GetListByPostId(postId);
        }


        // POST: api/PostDetails
        [HttpPost]
        public PostDetail Post([FromBody] PostDetailEntity obj)
        {
            PostDetail postDetail = PostDetailBuilder.Build(obj);
            return this.PostDetailRepository.Insert(postDetail);
        }

        // PUT: api/PostDetails/5
        [HttpPut("{id}")]
        public PostDetail Put(int id, [FromBody] PostDetailEntity obj)
        {
            PostDetail postDetail = PostDetailBuilder.Build(obj);
            return this.PostDetailRepository.Update(postDetail, id);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.PostDetailRepository.Delete(id);
        }
    }
}
