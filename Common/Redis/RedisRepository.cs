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

        public async Task<bool> Add(string id, T entity, int timesecstolive)
        {
            return await redis.StringSetAsync(id, JsonSerializer.Serialize(entity), TimeSpan.FromSeconds(timesecstolive));
        }

  
        public async Task<bool> Add(string id, object entity, int timesecstolive)
        {
            //JsonSerializer.Serialize should preserve casing of the json object's property
            //
            return await redis.StringSetAsync(id, JsonSerializer.Serialize(entity, new JsonSerializerOptions { PropertyNameCaseInsensitive =true }), TimeSpan.FromSeconds(timesecstolive));
        }

        public async Task<bool> DeleteById(string id)
        {
            return await redis.KeyDeleteAsync(id);
        }

        public async Task<T> GetById(string id)
        {
            var returnvalue = await redis.StringGetAsync(id);
            if (returnvalue.IsNullOrEmpty)
                return default(T);
            else
                return JsonSerializer.Deserialize<T>(returnvalue);
        }

        public async Task<string> GetStringById(string id)
        {
            var returnvalue = await redis.StringGetAsync(id);
            return returnvalue.ToString(); 
        }
    }
}
