using api.Models;

namespace api.Services
{
    public interface IQueueService
    {
        bool Enqueue(string name);
    }
}