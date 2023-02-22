using System.Security.Claims;
using backend.Dto;
using backend.Models;

namespace backend.Services;

public class PostService : IPostService
{
    private readonly DataContext _dataContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PostService(DataContext dataContext, IHttpContextAccessor httpContextAccessor)
    {
        _dataContext = dataContext;
        _httpContextAccessor = httpContextAccessor;
    }

    public Post CreatePost(PostCreateUpdateDto post)
    {
        if (String.IsNullOrEmpty(post.Title.Trim(' ')) || String.IsNullOrEmpty(post.Content.Trim(' '))) return null;

        Post createdPost = new Post();
        createdPost.Title = post.Title;
        createdPost.Content = post.Content;
        createdPost.Author = _dataContext.Users.Where(u => u.Email == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)).FirstOrDefault();
        
        _dataContext.Add(createdPost);
        _dataContext.SaveChanges();
        return createdPost;
    }

    public Post UpdatePost(PostCreateUpdateDto post, string postId)
    {
        Guid id = Guid.Parse(postId);
        var updatedPost = _dataContext.Posts.Where(p => p.Id == id).FirstOrDefault();
        if (updatedPost is null) return null;
        if (String.IsNullOrEmpty(post.Title.Trim(' ')) || String.IsNullOrEmpty(post.Content.Trim(' '))) return null;
        updatedPost.Title = post.Title;
        updatedPost.Content = post.Content;
        _dataContext.SaveChanges();
        return updatedPost;
    }
}
