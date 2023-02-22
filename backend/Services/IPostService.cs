using backend.Dto;
using backend.Models;

namespace backend.Services;

public interface IPostService{
    Post CreatePost(PostCreateUpdateDto post);
    Post UpdatePost(PostCreateUpdateDto post, string postId);
}