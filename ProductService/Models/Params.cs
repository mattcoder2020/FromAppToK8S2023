using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models
{
    public class QueryParams
    {
        public int? ProductCategoryId { get; set; }
        public string OrderBy { get; set; }
    }
}
