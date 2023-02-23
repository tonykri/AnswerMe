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

    public Post CreatePost(PostCreateUpdateDto post)
    {
        if (String.IsNullOrEmpty(post.Title.Trim(' ')) || String.IsNullOrEmpty(post.Content.Trim(' '))) return null;

        Post createdPost = new Post();
        createdPost.Title = post.Title.Trim(' ');
        createdPost.Content = post.Content.Trim(' ');
        createdPost.Author = _dataContext.Users.Where(u => u.Email == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)).FirstOrDefault();
        
        _dataContext.Add(createdPost);
        _dataContext.SaveChanges();
        return createdPost;
    }

    public IEnumerable<Post> GetUserPosts(string userId)
    {
        Guid id = Guid.Parse(userId);
        return _dataContext.Posts.Where(p => p.Author.Id == id).Include(p => p.Author);
    }

     public IEnumerable<Post> GetAllPosts()
    {
        return _dataContext.Posts.Include(p => p.Author);
    }

    public Post UpdatePost(PostCreateUpdateDto post, string postId)
    {
        Guid id = Guid.Parse(postId);
        var updatedPost = _dataContext.Posts.Where(p => p.Id == id).FirstOrDefault();
        var user = _dataContext.Users.Where(u => u.Email == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)).FirstOrDefault();
        if (updatedPost is null) return null;
        if (updatedPost.Author.Id != user.Id) return null;
        if (String.IsNullOrEmpty(post.Title.Trim(' ')) || String.IsNullOrEmpty(post.Content.Trim(' '))) return null;
        updatedPost.Title = post.Title.Trim(' ');
        updatedPost.Content = post.Content.Trim(' ');
        _dataContext.SaveChanges();
        return updatedPost;
    }

    public Comment CreateComment(CommentCreateUpdateDto comment, string postId){
        Guid id = Guid.Parse(postId);
        var post = _dataContext.Posts.Where(p => p.Id == id).FirstOrDefault();
        if (post is null) return null;
        Comment createdComment = new Comment();
        createdComment.Content = comment.Content.Trim(' ');
        createdComment.Author = _dataContext.Users.Where(u => u.Email == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)).FirstOrDefault();
        createdComment.CommentedPost = post;
        _dataContext.Add(createdComment);
        _dataContext.SaveChanges();
        return createdComment;
    }

    public Comment UpdateComment(CommentCreateUpdateDto comment, string commentId){
        Guid id = Guid.Parse(commentId);
        var commentToUpdate = _dataContext.Comments.Where(c => c.Id == id).FirstOrDefault();
        if (commentToUpdate is null) return null;
        var user = _dataContext.Users.Where(u => u.Email == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)).FirstOrDefault();
        if (commentToUpdate.Author.Id != user.Id) return null;
        commentToUpdate.Content = comment.Content.Trim(' ');
        _dataContext.SaveChanges();
        return commentToUpdate;
    }

    public Comment DeleteComment(string commentId){
        Guid id = Guid.Parse(commentId);
        var commentToDelete = _dataContext.Comments.Where(c => c.Id == id).FirstOrDefault();
        if (commentToDelete is null) return null;
        var user = _dataContext.Users.Where(u => u.Email == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)).FirstOrDefault();
        if (commentToDelete.Author.Id != user.Id) return null;
        _dataContext.Remove(commentToDelete);
        _dataContext.SaveChanges();
        return commentToDelete;
    }

    public Vote VoteComment(string postId, bool vote){
        Guid id = Guid.Parse(postId);
        var post = _dataContext.Posts.Where(p => p.Id == id).FirstOrDefault();
        if (post is null) return null;
        var user = _dataContext.Users.Where(u => u.Email == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)).FirstOrDefault();
        var v = _dataContext.Votes.Where(v => v.Author.Id == user.Id && v.VotedPost.Id == post.Id).FirstOrDefault();
        if (v is not null) {
            if (v.Agreed == vote) _dataContext.Remove(v);
            else v.Agreed = vote;
            _dataContext.SaveChanges();
            return v;
        }
        Vote createdVote = new Vote();
        createdVote.Author = user;
        createdVote.VotedPost = post;
        createdVote.Agreed = vote;
        _dataContext.Add(createdVote);
        _dataContext.SaveChanges();
        return createdVote;
    }
}
