using backend.Dto;
using backend.Models;
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

    [Authorize]
    [HttpPost("create")]
    public IActionResult Create([FromBody] PostCreateUpdateDto post){
        MsgStatus res = _postService.Create(post);
        if (res.StatusCode != 200) return BadRequest(res);
        return Ok(res);
    }

    [Authorize]
    [HttpDelete("delete/{postId}")]
    public IActionResult Delete([FromRoute] string postId){
        MsgStatus res = _postService.Delete(postId);
        if (res.StatusCode != 200) return BadRequest(res);
        return Ok(res);
    }

    [Authorize]
    [HttpPut("update/{postId}")]
    public IActionResult Update([FromBody] PostCreateUpdateDto post, [FromRoute] string postId){
        MsgStatus res = _postService.Update(post, postId);
        if (res.StatusCode != 200) return BadRequest(res);
        return Ok(res);
    }

    [Authorize]
    [HttpGet("viewAllPosts")]
    public IActionResult ViewAll(){
        ICollection<Post> res = _postService.ViewAll();
        return Ok(res);
    }

    [Authorize]
    [HttpGet("{postId}")]
    public IActionResult View([FromRoute] string postId){
        var res = _postService.View(postId);
        if (res is MsgStatus) return BadRequest(res);
        return Ok(res);
    }
}
