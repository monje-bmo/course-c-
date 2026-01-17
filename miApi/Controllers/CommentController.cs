using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using miApi.Interfaces;
using miApi.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace miApi.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentepository _comment_repo;
        public CommentController(ICommentepository commentepository)
        {
            _comment_repo = commentepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _comment_repo.GetAllAsync();
            var commentDTO = comments.Select(c => c.ToCommentDto());
            return Ok(commentDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _comment_repo.GetByIdAsync(id);
            if(comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        
        }

    }
}