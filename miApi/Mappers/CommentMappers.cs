using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using miApi.Dtos.Comment;
using miApi.models;

namespace miApi.Mappers
{
    public static class CommentMappers
    {
        public static CommentDTO ToCommentDto(this Comment commentModel)
        {
            return new CommentDTO
            {
                Id = commentModel.Id,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId,
                Title = commentModel.Title,

            };
        }
    }
}