using backend.Dto;

namespace backend.Services;

public interface ICommentService{
    MsgStatus Create(CommentCreateUpdateDto comment, string postId);
    MsgStatus Update(CommentCreateUpdateDto comment, string commentId);
    MsgStatus Delete(string commentId);
    MsgStatus Vote(string commentId, bool agree);
}