using System.Security.Claims;
using backend.Dto;
using backend.Models;

namespace backend.Services;

public class CommentService : ICommentService
{
    private readonly DataContext _dataContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CommentService(DataContext dataContext, IHttpContextAccessor httpContextAccess)
    {
        _dataContext = dataContext;
        _httpContextAccessor = httpContextAccess;
    }

    public MsgStatus Create(CommentCreateUpdateDto comment, string postId)
    {
        comment.Content = comment.Content.Trim();
        if (comment.Content.Length < 1) return new MsgStatus("Comment cannot be empty", 400);
        Post post = _dataContext.Posts.Where(p => p.Id == Guid.Parse(postId)).FirstOrDefault();
        if (post is null) return new MsgStatus("Post does not exist", 404);

        Comment com = new Comment();
        com.Content = comment.Content;
        com.Post = post;
        com.User = _dataContext.Users.Where(u => u.Email == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)).FirstOrDefault();

        _dataContext.Add(com);
        _dataContext.SaveChanges();
        return new MsgStatus("Comment created", 200);
    }


    public MsgStatus Update(CommentCreateUpdateDto comment, string commentId)
    {
        comment.Content = comment.Content.Trim();
        if (comment.Content.Length < 1) return new MsgStatus("Comment cannot be empty", 400);

        Comment com = _dataContext.Comments.Where(c => c.Id == Guid.Parse(commentId)).FirstOrDefault();
        if (com is null) return new MsgStatus("Comment does not exist", 404);

        User user = _dataContext.Users.Where(u => u.Email == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)).FirstOrDefault();
        if (!com.User.Email.Equals(user.Email)) return new MsgStatus("Cannot update this comment", 401);

        com.Content = comment.Content;
        com.Updated = DateTime.Now;
        _dataContext.SaveChanges();
        return new MsgStatus("Comment updated", 200);
    }


    public MsgStatus Delete(string commentId)
    {
        Comment com = _dataContext.Comments.Where(c => c.Id == Guid.Parse(commentId)).FirstOrDefault();
        if (com is null) return new MsgStatus("Comment does not exist", 404);

        User user = _dataContext.Users.Where(u => u.Email == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)).FirstOrDefault();
        if (!com.User.Email.Equals(user.Email)) return new MsgStatus("Cannot delete this comment", 401);

        _dataContext.Remove(com);
        _dataContext.SaveChanges();
        return new MsgStatus("Comment deleted", 200);
    }


    public MsgStatus Vote(string commentId, bool agree)
    {
        Comment com = _dataContext.Comments.Where(c => c.Id == Guid.Parse(commentId)).FirstOrDefault();
        if (com is null) return new MsgStatus("Comment does not exist", 404);
        User user = _dataContext.Users.Where(u => u.Email == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email)).FirstOrDefault();

        Vote vote = _dataContext.Votes.Where(v => v.Comment.Id == com.Id && v.User.Email == user.Email).FirstOrDefault();
        if (vote is null)
        {
            Vote newVote = new Vote();
            newVote.Comment = com;
            newVote.Agreed = agree;
            newVote.User = user;
            _dataContext.Add(newVote);
        }
        else
        {
            if (vote.Agreed == agree) _dataContext.Remove(vote);
            else vote.Agreed = agree;
        }
        _dataContext.SaveChanges();
        return new MsgStatus("Ok", 200);
    }
}