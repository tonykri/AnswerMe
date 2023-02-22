using backend.Dto;
using backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostController : ControllerBase{
    
    private readonly DataContext _dataContext;
    private readonly IPostService _postService;

    public PostController(DataContext dataContext, IPostService postService) {
        _dataContext = dataContext;
        _postService = postService;
    }

    [HttpPost("create")]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] PostCreateUpdateDto post) {
        var createdPost = _postService.CreatePost(post);
        if (createdPost is null) return BadRequest("Something went wrong");
        return Ok(createdPost);
    }

    [HttpPut("update/{postId}")]
    [Authorize]
    public async Task<IActionResult> Update([FromBody] PostCreateUpdateDto post, [FromRoute] string postId) {
        var updatedPost = _postService.UpdatePost(post, postId);
        if (updatedPost is null) return BadRequest("Something went wrong");
        return Ok(updatedPost);
    }
}
