using backend.Dto;
using backend.Models;

namespace backend.Services;

public interface IPostService
{
    MsgStatus Create(PostCreateUpdateDto post);
    MsgStatus Delete(string postId);
    MsgStatus Update(PostCreateUpdateDto post, string postId);
    ICollection<Post> ViewAll();
    object View(string postId);

}