using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProductAPI.Core.DTOs
{
    public class ProductFilterDto
    {
        public string SearchTerm { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; } = "asc"; // Default to ascending order
    }
}
