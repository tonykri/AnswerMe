using System.Security.Claims;
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
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PostController(DataContext dataContext, IPostService postService, IHttpContextAccessor httpContextAccessor) {
        _dataContext = dataContext;
        _postService = postService;
        _httpContextAccessor = httpContextAccessor;
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

    [HttpGet("viewUser/{userId}")]
    [Authorize]
    public async Task<IActionResult> ViewUserPost([FromRoute] string userId) {
        return Ok(_postService.GetUserPosts(userId));
    }

    [HttpGet("viewMyPosts")]
    [Authorize]
    public async Task<IActionResult> ViewMyPosts() {
        string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Sid);
        return Ok(_postService.GetUserPosts(userId));
    }

    [HttpGet("viewAllPosts")]
    [Authorize]
    public async Task<IActionResult> ViewAllPosts() {
        return Ok(_postService.GetAllPosts());
    }

    [HttpPost("createComment/{postId}")]
    [Authorize]
    public async Task<IActionResult> CreateComment([FromBody] CommentCreateUpdateDto comment, [FromRoute] string postId) {
        var com = _postService.CreateComment(comment, postId);
        if (com is null) return BadRequest("Something went wrong");
        return Ok(com);
    }

    [HttpPut("updateComment/{commentId}")]
    [Authorize]
    public async Task<IActionResult> UpdateComment([FromBody] CommentCreateUpdateDto comment, [FromRoute] string commentId) {
        var com = _postService.UpdateComment(comment, commentId);
        if (com is null) return BadRequest("Something went wrong");
        return Ok(com);
    }

    [HttpDelete("deleteComment/{commentId}")]
    [Authorize]
    public async Task<IActionResult> DeleteComment([FromRoute] string commentId) {
        var com = _postService.DeleteComment(commentId);
        if (com is null) return BadRequest("Something went wrong");
        return Ok(com);
    }
}
