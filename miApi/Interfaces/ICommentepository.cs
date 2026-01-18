using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using miApi.Dtos.Comment;
using miApi.models;

namespace miApi.Interfaces
{
    public interface ICommentepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment commentModel);
    }
}