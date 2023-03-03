using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Models
{
    public class Basket : Product
    {
        public Basket()
        {
            if (String.IsNullOrEmpty(BasketId))
            {
                BasketId = Guid.NewGuid().ToString();
            }
        }
        public string BasketId { get; set; }
    }
}
