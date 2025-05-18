using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VowelBonus.Shared.DTOs
{
    public abstract class BaseFilterDto
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public int Skip => Math.Max(0, (Page - 1) * PageSize);
        public string? SortBy { get; set; }
        public string? SortDirection { get; set; } = "asc";
    }
}
