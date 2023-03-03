using Common.Redis;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.DataAccess
{
    public class BasketRedisRespository<Basket>: RedisRepository<Basket>, IRedisRepository<Basket>
    {
        public BasketRedisRespository(IConnectionMultiplexer redis): base(redis)
        {

        }
    }
}
