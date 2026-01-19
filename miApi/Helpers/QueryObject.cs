using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace miApi.Helpers
{
    public class QueryObject
    {
        public string? Symblo { get; set; } = null;
        public string? CompanyName { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsDecsending { get; set; } = false;
    }
}