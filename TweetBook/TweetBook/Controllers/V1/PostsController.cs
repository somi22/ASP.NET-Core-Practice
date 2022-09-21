using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TweetBook.Domain;
using TweetBook.Contracts.V1;
using System;
using TweetBook.Contracts.V1.Requests;
using TweetBook.Contracts.V1.Responses;

namespace Tweetbook.Controllers.V1
{
    public class PostsController : Controller
    {
        private List<Post> _posts;

        public PostsController()
        {
            _posts = new List<Post>();
            for (var i = 0; i < 5; i++)
            {
                _posts.Add(new Post { Id = Guid.NewGuid().ToString() });
            }
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_posts);
        }



        [HttpPost(ApiRoutes.Posts.Create)]
        public IActionResult Create([FromBody] CreatePostRequest postRequest) 
        {
            // Request와 Domain obj을 mapping 하는 과정  
            // Contract와 Domain obj를 app memory mixup하는 것을 방지하고 version 관리하기 위함
            var post = new Post { Id = postRequest.Id };

            // only for practice!! 
            if (string.IsNullOrEmpty(post.Id))
                post.Id = Guid.NewGuid().ToString();
            _posts.Add(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}", post.Id);

            var response = new PostResponse { Id = post.Id };

            return Created(locationUri, post);
        }
    }
}

