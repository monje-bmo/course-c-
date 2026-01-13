using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace miApi.models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        //Foreign key
        public int? StockId { get; set; }
        //Navigation property
        public Stock? Stock { get; set; }

    }
}