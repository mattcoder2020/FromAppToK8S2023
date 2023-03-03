using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Common.Redis
{
    public class RedisRepository<T> : IRedisRepository<T>
    {
        private readonly IDatabase redis;

        public RedisRepository(IConnectionMultiplexer redis)
        {
            this.redis = redis.GetDatabase();
        }

        public async Task<bool> Add(string id, T entity)
        {
            return await redis.StringSetAsync(id, JsonSerializer.Serialize(entity), TimeSpan.FromDays(3));
        }

        public async Task<bool> DeleteById(string id)
        {
            return await redis.KeyDeleteAsync(id);
        }

        public async Task<T> GetById(string id)
        {
            var returnvalue = await redis.StringGetAsync(id);
            return returnvalue.IsNullOrEmpty ? default(T) : JsonSerializer.Deserialize<T>(returnvalue);
        }
    }
}
