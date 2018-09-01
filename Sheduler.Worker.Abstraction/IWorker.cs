using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sheduler.Worker.Abstraction
{
    public interface IWorker
    {
        IServiceCollection Services { get; }
        Task<string> StartAsync();
    }
}
