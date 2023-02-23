using backend.Dto;
using backend.Models;

namespace backend.Services;

public interface IPostService{
    Post CreatePost(PostCreateUpdateDto post);
    Post UpdatePost(PostCreateUpdateDto post, string postId);
    IEnumerable<Post> GetUserPosts(string userId);
    IEnumerable<Post> GetAllPosts();
    Comment CreateComment(CommentCreateUpdateDto comment, string postId);
    Comment UpdateComment(CommentCreateUpdateDto comment, string commentId);
    Comment DeleteComment(string commentId);
}