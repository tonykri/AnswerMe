using System.Security.Claims;
using backend.Dto;
using backend.Models;
using Microsoft.EntityFrameworkCore;

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

    public MsgStatus Create(PostCreateUpdateDto post)
    {
        post.Title = post.Title.Trim();
        post.Content = post.Content.Trim();
        if (post.Title.Length < 3 || post.Content.Length < 3) return new MsgStatus("Fields cannot be blank", 400);

        Post p = new Post();
        p.Title = post.Title;
        p.Content = post.Content;
        p.User = _dataContext.Users.Where(u => u.Email == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)).FirstOrDefault();
        _dataContext.Add(p);
        p.User.Posts.Add(p);
        _dataContext.SaveChanges();
        return new MsgStatus("Post created", 200);
    }

    public MsgStatus Delete(string postId)
    {
        Post post = _dataContext.Posts.Where(p => p.Id == Guid.Parse(postId)).FirstOrDefault();
        if (post is null) return new MsgStatus("Post not found", 404);

        User user = _dataContext.Users.Where(u => u.Email == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)).FirstOrDefault();
        if (!post.User.Email.Equals(user.Email)) return new MsgStatus("Cannot delete this post", 401);

        _dataContext.Remove(post);
        _dataContext.SaveChanges();
        return new MsgStatus("Post deleted", 200);
    }


    public MsgStatus Update(PostCreateUpdateDto post, string postId)
    {
        post.Title = post.Title.Trim();
        post.Content = post.Content.Trim();
        if (post.Title.Length < 3 || post.Content.Length < 3) return new MsgStatus("Fields cannot be blank", 400);

        Post p = _dataContext.Posts.Where(p => p.Id == Guid.Parse(postId)).FirstOrDefault();
        if (p is null) return new MsgStatus("Post not found", 404);

        User user = _dataContext.Users.Where(u => u.Email == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)).FirstOrDefault();
        if (!p.User.Email.Equals(user.Email)) return new MsgStatus("Cannot update this post", 401);

        p.Title = post.Title;
        p.Content = post.Content;
        p.Updated = DateTime.Now;
        _dataContext.SaveChanges();
        return new MsgStatus("Post updated", 200);
    }


    public ICollection<Post> ViewAll(){
        return _dataContext.Posts.Include(p => p.User).OrderByDescending(p => p.Created).ToList();
    }


    public object View(string postId){
        Post post = _dataContext.Posts.Where(p => p.Id == Guid.Parse(postId))
            .Include(p => p.User)
            .Include(p => p.Comments).ThenInclude(c => c.User)
            .Include(p => p.Comments).ThenInclude(c => c.Votes)
            .FirstOrDefault();
        if (post is null) return new MsgStatus("Post not found", 404);
        return post;
    }
}
