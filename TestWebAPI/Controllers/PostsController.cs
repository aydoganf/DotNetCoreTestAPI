using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestBussiness.Connection;
using TestBussiness.Entity;
using TestBussiness.Repository;
using TestBussiness.RepositoryService;
using TestWebAPI.Builders;
using TestWebAPI.Models;

namespace TestWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        public INHibernateHelper NHibernateHelper;
        public IPostRepository PostRepository;

        public PostsController(INHibernateHelper nHibernateHelper, IPostRepository postRepository)
        {
            this.NHibernateHelper = nHibernateHelper;
            this.PostRepository = postRepository;
        }

        // GET: api/Posts
        [HttpGet]
        public IEnumerable<Post> Get()
        {
            return this.PostRepository.GetAll();
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public Post Get(int id)
        {
            return this.PostRepository.GetById(id);
        }

        [HttpGet("author/{authorName}")]
        public IEnumerable<Post> Get(string authorName)
        {
            return this.PostRepository.GetByAuthorname(authorName);
        }

        // POST: api/Posts
        [HttpPost]
        public Post Post([FromBody] PostEntity obj)
        {
            Post post = PostBuilder.Build(obj);
            return this.PostRepository.Insert(post);
        }

        // PUT: api/Posts/5
        [HttpPut("{id}")]
        public Post Put(int id, [FromBody] PostEntity obj)
        {
            Post post = PostBuilder.Build(obj);
            post = this.PostRepository.Update(post, id);
            return post ?? null;
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.PostRepository.Delete(id);
        }
    }
}
