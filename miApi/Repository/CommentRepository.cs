using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using miApi.Data;
using miApi.Dtos.Comment;
using miApi.Interfaces;
using miApi.models;
using Microsoft.EntityFrameworkCore;

namespace miApi.Repository
{
    public class CommentRepository : ICommentepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateAsync(Comment commentModel)
        {
            await _context.Comments.AddAsync(commentModel);
            await _context.SaveChangesAsync();
            return commentModel;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<Comment?> UpdateCommentAsync(int id, Comment updateCommentDto)
        {
            var commentModel = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);

            if (commentModel == null)
            {
                return null;
            }

            commentModel.Title = updateCommentDto.Title;
            commentModel.Content = updateCommentDto.Content;

            await _context.SaveChangesAsync();
            return commentModel; 

        }
    }
}