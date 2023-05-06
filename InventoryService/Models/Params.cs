using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryService.Models
{
    public class QueryParams
    {
        public int? ProductCategoryId { get; set; }
        public int? ProductId { get; set; }
    }
}
