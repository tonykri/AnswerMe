
using backend.Dto;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase{
    
    private readonly DataContext _dataContext;
    private readonly ICommentService _commentService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CommentController(DataContext dataContext, ICommentService commentService, IHttpContextAccessor httpContextAccessor) {
        _dataContext = dataContext;
        _commentService = commentService;
        _httpContextAccessor = httpContextAccessor;
    }


    [HttpPost("create/{postId}")]
    public ActionResult Create([FromBody] CommentCreateUpdateDto comment, [FromRoute] string postId)
    {
        MsgStatus res = _commentService.Create(comment, postId);
        if (res.StatusCode != 200) return BadRequest(res);
        return Ok(res);
    }


    [HttpPut("update/{commentId}")]
    public ActionResult Update([FromBody] CommentCreateUpdateDto comment, [FromRoute] string commentId)
    {
        MsgStatus res = _commentService.Update(comment, commentId);
        if (res.StatusCode != 200) return BadRequest(res);
        return Ok(res);
    }
    

    [HttpDelete("{commentId}")]
    public ActionResult Delete([FromRoute] string commentId)
    {
        MsgStatus res = _commentService.Delete(commentId);
        if (res.StatusCode != 200) return BadRequest(res);
        return Ok(res);
    }
    
}