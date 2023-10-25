using cricketBackend.Dto;
using cricketBackend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cricketBackend.Services
{
    public interface IPostService
    {
        Task<PostDto> CreatePost(CreatePostDto createPost);
        Task<List<Post>> GetAllPost();
        Task<bool> LikePost(LikePostDto likePost);
        Task<bool> CommentPost(CommentPostDto commentPost);
    }
}
