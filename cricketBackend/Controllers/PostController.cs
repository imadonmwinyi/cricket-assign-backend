using cricketBackend.Dto;
using cricketBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cricketBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
   
        private readonly ILogger<PostController> _logger;
        private readonly IPostService _postService;

        public PostController(ILogger<PostController> logger, IPostService postService)
        {
            _logger = logger;
            _postService = postService;
        }

        [HttpPost]
        [Route("createPost")]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostDto createPost)
        {
            ResponseDto response = new ResponseDto();
            try
            {
              
                if(createPost == null)
                {
                    response.Success = false;
                    response.Message = "Post cannot be Empty";
                    return BadRequest(response);
                   
                }
                var postDto = await _postService.CreatePost(createPost);
                if (postDto == null)
                {
                    response.Success = false;
                    response.Message = "Could not Create Post";
                    return BadRequest(response);
                   
                }

                response.Success = true;
                response.Message = "Post Created";
                response.Data = postDto;
                return Ok(response);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Could not Create Post";
                _logger.LogError("Log Create Post Failed: ", ex.Message);
                return BadRequest(response);
            }
        }

        [HttpPost]
        [Route("commentPost")]
        public async Task<IActionResult> CommentPost([FromBody] CommentPostDto commentPost)
        {
            ResponseDto response = new ResponseDto();
            try
            {

                if (commentPost == null)
                {
                    response.Success = false;
                    response.Message = "Comment Post cannot be Empty";
                    return BadRequest(response);

                }
                var isComment = await _postService.CommentPost(commentPost);
                if (isComment == false)
                {
                    response.Success = false;
                    response.Message = "Could not Create Comment";
                    return BadRequest(response);

                }

                response.Success = true;
                response.Message = "Comment Created";
                return Ok(response);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Could not Create Comment";
                _logger.LogError("Log Create Comment Failed: ", ex.Message);
                return BadRequest(response);
            }
        }

        [HttpPost]
        [Route("likePost")]
        public async Task<IActionResult> LikePost([FromBody] LikePostDto likePost)
        {
            ResponseDto response = new ResponseDto();
            try
            {

                if (likePost == null)
                {
                    response.Success = false;
                    response.Message = "Like Post cannot be Empty";
                    return BadRequest(response);

                }
                var isLikePost = await _postService.LikePost(likePost);
                if (isLikePost == false)
                {
                    response.Success = false;
                    response.Message = "Could not Add Like to Post";
                    return BadRequest(response);

                }

                response.Success = true;
                response.Message = "Like Added to Post";
                return Ok(response);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Could not Add Like to Post";
                _logger.LogError("Log Add Like Failed: ", ex.Message);
                return BadRequest(response);
            }
        }

        [HttpGet]
        [Route("getAllPost")]
        public async Task<IActionResult> GetAllPost()
        {
            ResponseDto response = new ResponseDto();
            try
            {

                var posts = await _postService.GetAllPost();

                response.Success = true;
                response.Message = "All Post";
                response.Data = posts;
                return Ok(response);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Couldn't get Posts";
                _logger.LogError("Log Get Posts Failed: ", ex.Message);
                return BadRequest(response);
            }
        }

        [HttpGet]
        [Route("downloadCsv")]
        public async Task<IActionResult> DownloadCsv()
        {
            ResponseDto response = new ResponseDto();
            try
            {

                var posts = await _postService.GetAllPost();

                response.Success = true;
                response.Message = "All Post";
                response.Data = posts;
                return Ok(response);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Couldn't get Posts";
                _logger.LogError("Log DownLoad CSV Failed: ", ex.Message);
                return BadRequest(response);
            }
        }
    }
}
