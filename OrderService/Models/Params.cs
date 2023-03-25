using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Models
{
    public class OrderQueryParams
    {
        public int? ProductCategoryId { get; set; }
        public string OrderBy { get; set; }
        public int? ProductId { get; set; }
    }
}
