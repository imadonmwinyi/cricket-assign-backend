using cricketBackend.Dto;
using cricketBackend.Model;
using cricketBackend.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cricketBackend.Services
{
    public class PostService : IPostService
    {
        private readonly IGenericRepository<Post> _genericPostRepo;
        private readonly IGenericRepository<Like> _genericLikeRepo;
        private readonly IGenericRepository<Comment> _genericCommentRepo;
        public PostService(IGenericRepository<Post> genericPostRepo, IGenericRepository<Like> genericLikeRepo, IGenericRepository<Comment> genericCommentRepo)
        {
            _genericCommentRepo = genericCommentRepo;
            _genericLikeRepo = genericLikeRepo;
            _genericPostRepo = genericPostRepo;
        }
        public async Task<bool> CommentPost(CommentPostDto commentPost)
        {
            try
            {
                bool isCommentInserted = false;
                Comment comment = new Comment
                {
                    Text = commentPost.Text,
                    DateAdded = DateTime.Now,
                    PostId = commentPost.PostId
                };
                var commentedPost = await _genericCommentRepo.Insert(comment);

               if(commentedPost.Entity.Id > 0)
                {
                    isCommentInserted = true;
                }

                await _genericPostRepo.Save();

                return isCommentInserted;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Log Error Comment Post: {0}", ex.Message);
                throw;
            }
        }

        public async Task<PostDto> CreatePost(CreatePostDto createPost)
        {
            try
            {
                PostDto responseDto = new PostDto();
                Post post = new Post
                {
                    Text = createPost.Text,
                    Title = createPost.Title,
                    DateAdded = DateTime.Now
                };
                var insertedPost =  await _genericPostRepo.Insert(post);

                responseDto.Id = insertedPost.Entity.Id;
                responseDto.Title = insertedPost.Entity.Title;
                responseDto.Text = insertedPost.Entity.Text;
                responseDto.DateAdded = insertedPost.Entity.DateAdded;

                await _genericPostRepo.Save();

                return responseDto;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Log Error Create Post: {0}", ex.Message);
                throw;
            }
        }

        public async Task<List<Post>> GetAllPost()
        {
            try
            {
                List<Post> posts = new List<Post>();
                posts = await _genericPostRepo.GetAll();
                return posts;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Log Error Get All Post: {0}", ex.Message);
                throw;
            };
        }

        public async Task<bool> LikePost(LikePostDto likePost)
        {
            try
            {
                bool isLikeInserted = false;
                Like like = new Like
                {
                    
                    DateAdded = DateTime.Now,
                    PostId = likePost.PostId
                };
                var commentedPost = await _genericLikeRepo.Insert(like);

                if (commentedPost.Entity.Id > 0)
                {
                    isLikeInserted = true;
                }

                await _genericPostRepo.Save();

                return isLikeInserted;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Log Error LikePost: {0}", ex.Message);
                throw;
            }
        }
    }
}
