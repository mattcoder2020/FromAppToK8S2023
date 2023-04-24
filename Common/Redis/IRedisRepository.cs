using System.Threading.Tasks;

namespace Common.Redis
{
    public interface IRedisRepository<T>
    {
        Task<bool> Add(string id, T entity);
        Task<bool> Add(string id, object entity, int timesecstolive);
        Task<bool> DeleteById(string id);
        Task<T> GetById(string id);
        Task<string> GetStringById(string id);
    }
}