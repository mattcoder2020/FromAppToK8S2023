using System.Threading.Tasks;
using Common.Messages;

namespace ISignalRService.Services
{
    public interface IiSignalRService
    {
        Task Publish(RejectedEvent evt);
    }
}