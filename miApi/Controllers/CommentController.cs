using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using miApi.Dtos.Comment;
using miApi.Interfaces;
using miApi.Mappers;
using miApi.models;
using Microsoft.AspNetCore.Mvc;

namespace miApi.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentepository _comment_repo;
        private readonly IStockRepository _stock_repo;
        public CommentController(ICommentepository commentepository, IStockRepository stockRepository)
        {
            _comment_repo = commentepository;
            _stock_repo = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var comments = await _comment_repo.GetAllAsync();
            var commentDTO = comments.Select(c => c.ToCommentDto());
            return Ok(commentDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var comment = await _comment_repo.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());

        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto createCommentDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            if (!await _stock_repo.StockExists(stockId))
            {
                return BadRequest("Stock does not exist");
            }

            var commentModel = createCommentDto.ToCommentFromCreate(stockId);
            await _comment_repo.CreateAsync(commentModel);

            return CreatedAtAction(nameof(GetById), new { id = commentModel }, commentModel.ToCommentDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto updateCommentDto)
        {

            if(!ModelState.IsValid)
                return BadRequest();

            var comment = await _comment_repo.UpdateCommentAsync(id, updateCommentDto.ToUpdateFromComment());
            if (comment == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var comment = await _comment_repo.DeleteCommentAsync(id);
            if (comment == null)
            {
                return NotFound("Comment not found");
            }

            return Ok(comment);
        }

    }
}