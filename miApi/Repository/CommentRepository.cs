using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using miApi.Data;
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
        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }
    }
}