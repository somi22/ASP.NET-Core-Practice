using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TweetBook.Domain;
using TweetBook.Contracts.V1;
using System;
using TweetBook.Contracts.V1.Requests;
using TweetBook.Contracts.V1.Responses;
using System.Linq;
using TweetBook.Services;

namespace Tweetbook.Controllers.V1
{
    public class PostsController : Controller
    {
        // controller에 hit 할때마다, 생성되고 있음 => service와 같은 곳에 extract 하는 것이 좋다. and register it as a singleton
        // 아래 코드 Services > PostService.cs로 이동
        //private readonly List<Post> _posts;

        private readonly IPostService _postService;
        public PostsController(IPostService postService)
        {
            _postService = postService;
            // _posts = new List<Post>();
            // for (var i = 0; i < 5; i++)
            // {
            //     _posts.Add(new Post
            //     {
            //         Id = Guid.NewGuid(),
            //         Name = $"Post Name {i}"
            //
            //     });
            // }
        }

        [HttpGet(ApiRoutes.Posts.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_postService.GetPosts());
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        // ApiRoutes에 적은 `postId`이름과 동일해야함
        public IActionResult Get([FromRoute]Guid postId)
        {
            //matching 된 것이 있으면 보내고, 없으면 null
            var post = _postService.GetPostById(postId);

            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        
        [HttpPut(ApiRoutes.Posts.Update)]
        // ApiRoutes에 적은 `postId`이름과 동일해야함
        public IActionResult Update([FromRoute]Guid postId, [FromBody] UpdatePostRequest request)
        {
            var post = new Post
            {
                Id = postId,
                Name = request.Name
            };


            return Ok(post);
        }

        [HttpPost(ApiRoutes.Posts.Create)]
        public IActionResult Create([FromBody] CreatePostRequest postRequest) 
        {
            // Request와 Domain obj을 mapping 하는 과정  
            // Contract와 Domain obj를 app memory mixup하는 것을 방지하고 version 관리하기 위함
            var post = new Post { Id = postRequest.Id };

            // only for practice!! 
            if (post.Id != Guid.Empty)
                post.Id = Guid.NewGuid();
            _postService.GetPosts().Add(post);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("{postId}", post.Id.ToString());

            var response = new PostResponse { Id = post.Id };

            return Created(locationUri, response);
        }
    }
}
